using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;
using LogManager = NLog.LogManager;
using Screen = Caliburn.Micro.Screen;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof (AppViewModel))]
    public class AppViewModel : Conductor<Screen>.Collection.OneActive, IHaveDisplayName, IViewAware
    {
        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;

        public enum AutoTrainerModes
        {
            Controller,
            ActiveSession,
        }

        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private string _displayName = "PERSONAL TRAINER";
        private string _errorText;

        private AutoTrainerModes _autoTrainerMode;
        private readonly ControllerViewModel _controller;
        private SessionViewModel _session;

        public override string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        public bool IsSessionActive => AutoTrainerMode == AutoTrainerModes.ActiveSession;

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                NotifyOfPropertyChange(() => ErrorText);
            }
        }

        public AutoTrainerModes AutoTrainerMode
        {
            get { return _autoTrainerMode; }
            set
            {
                _autoTrainerMode = value;
                NotifyOfPropertyChange(() => AutoTrainerMode);
                NotifyOfPropertyChange(() => IsSessionActive);
            }
        }

        public AppViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;

            _controller = new ControllerViewModel(trainingSession, scriptExecutor);
            SetControlPanelState();
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

            _session = new SessionViewModel(Application.Current.Dispatcher, _trainingSession, _scriptExecutor, _controller.SelectedScript.ScriptFileName);
            _session.ScriptCompleted += OnScriptCompleted;

            ActivateItem(_session);
        }

        private void OnScriptCompleted(object sender, ScriptResultEventArgs args)
        {
            EndSession();

            if (args.Result.CompileExceptionInfo != null)
            {
                ErrorText = args.Result.CompileExceptionInfo.SourceException.Message;
            }
            else if (args.Result.ExecuteExceptionInfo != null)
            {
                ErrorText = args.Result.ExecuteExceptionInfo.SourceException.Message;
            }
        }

        private void EndSession()
        {
            _session.ScriptCompleted -= OnScriptCompleted;

            if (_session.IsActive)
            {
                DeactivateItem(_session, true);
            }

            SetControlPanelState();
            ActivateItem(_controller);
        }
    }
}