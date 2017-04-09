using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Scripting;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(AppViewModel))]
    public class AppViewModel : Conductor<Screen>.Collection.OneActive, IHaveDisplayName, IViewAware
    {
        public enum AutoTrainerModes
        {
            Controller,
            ActiveSession
        }

        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private readonly IHostedScriptExecutor _scriptExecutor;
        private readonly ITrainingSession _trainingSession;
        private readonly ControllerViewModel _controller;
        private AutoTrainerModes _autoTrainerMode;
        private string _displayName = Constants.PersonalTrainerTitle;
        private string _errorText;
        private SessionViewModel _session;

        public AppViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;

            _controller = new ControllerViewModel(trainingSession, scriptExecutor);
            SetControlPanelState();
        }

        public bool IsSessionActive => AutoTrainerMode == AutoTrainerModes.ActiveSession;

        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                NotifyOfPropertyChange(() => ErrorText);
            }
        }

        public AutoTrainerModes AutoTrainerMode
        {
            get => _autoTrainerMode;
            set
            {
                _autoTrainerMode = value;
                NotifyOfPropertyChange(() => AutoTrainerMode);
                NotifyOfPropertyChange(() => IsSessionActive);
            }
        }

        public override string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            ActivateItem(_controller);
        }

        private void SetControlPanelState()
        {
            AutoTrainerMode = AutoTrainerModes.Controller;
        }

        private void SetSessionState()
        {
            AutoTrainerMode = AutoTrainerModes.ActiveSession;
            ErrorText = string.Empty;
        }

        public void UserAction()
        {
            switch (AutoTrainerMode)
            {
                case AutoTrainerModes.Controller:
                    _logger.Info("User started session");
                    StartSession();
                    break;
                case AutoTrainerModes.ActiveSession:
                    // Button invisible at this point. Had intended to have a global "TAP OUT!" button.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartSession()
        {
            SetSessionState();

            _session = new SessionViewModel(Application.Current.Dispatcher, _trainingSession, _scriptExecutor,
                _controller.SelectedCollection.SelectedScript.ScriptFileName);
            _session.ScriptCompleted += OnScriptCompleted;

            ActivateItem(_session);
        }

        private void OnScriptCompleted(object sender, ScriptResultEventArgs args)
        {
            EndSession();

            if (args.Result.CompileExceptionInfo != null)
                ErrorText = args.Result.CompileExceptionInfo.SourceException.Message;
            else if (args.Result.ExecuteExceptionInfo != null)
                ErrorText = args.Result.ExecuteExceptionInfo.SourceException.Message;
        }

        private void EndSession()
        {
            _session.ScriptCompleted -= OnScriptCompleted;

            if (_session.IsActive)
                DeactivateItem(_session, true);

            SetControlPanelState();
            ActivateItem(_controller);
        }
    }
}