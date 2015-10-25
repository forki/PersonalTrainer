using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.Scripting;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof (AppViewModel))]
    public class AppViewModel : Conductor<Screen>.Collection.OneActive, IHaveDisplayName, IViewAware
    {
        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;

        private enum AutoTrainerModes
        {
            Controller,
            ActiveSession,
        }

        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private string _displayName = "AUTO TRAINER";
        private string _errorText;
        private string _actionText;

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

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                NotifyOfPropertyChange(() => ErrorText);
            }
        }

        public string ActionText
        {
            get { return _actionText; }
            set
            {
                _actionText = value;
                NotifyOfPropertyChange(() => ActionText);
            }
        }

        public AppViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;
            _controller = new ControllerViewModel();
            SetControlPanelState();
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            ActivateItem(_controller);
        }

        private void SetControlPanelState()
        {
            _autoTrainerMode = AutoTrainerModes.Controller;
            ActionText = "START!";
        }

        private void SetSessionState()
        {
            _autoTrainerMode = AutoTrainerModes.ActiveSession;
            ErrorText = string.Empty;
            ActionText = "TAP OUT";
        }


        public void UserAction()
        {
            switch (_autoTrainerMode)
            {
                case AutoTrainerModes.Controller:
                    _logger.Info("User started session");
                    StartSession();
                    break;
                case AutoTrainerModes.ActiveSession:
                    _logger.Info("User ended session");
                    EndSession();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartSession()
        {
            SetSessionState();

            _session = new SessionViewModel(_trainingSession, _scriptExecutor);
            _session.ScriptCompleted += OnScriptCompleted;
            ActivateItem(_session);
        }

        private void OnScriptCompleted(object sender, ScriptResultEventArgs args)
        {
            if (args.Result.CompileExceptionInfo != null)
            {
                ErrorText = args.Result.CompileExceptionInfo.SourceException.Message;
            }
            else if (args.Result.ExecuteExceptionInfo != null)
            {
                ErrorText = args.Result.ExecuteExceptionInfo.SourceException.Message;
            }

            EndSession();
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