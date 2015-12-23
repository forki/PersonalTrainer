using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Threading;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Content;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Voice;
using Action = System.Action;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof (SessionViewModel))]
    public sealed class SessionViewModel : Screen
    {
        private readonly NLog.Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private readonly Dispatcher _dispatcher;
        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;
        private readonly string _scriptFile;

        private string _imageLocation = string.Empty;
        private string _subtitle;
        private IDisposable _subscription;

        public string ImageLocation
        {
            get { return _imageLocation; }
            private set
            {
                _imageLocation = value;
                NotifyOfPropertyChange(() => ImageLocation);
            }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set
            {
                _subtitle = value;
                NotifyOfPropertyChange(() => Subtitle);
            }
        }

        public SessionViewModel(Dispatcher dispatcher, ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor,
            string scriptFile)
        {
            _dispatcher = dispatcher;
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;
            _scriptFile = scriptFile;
            _trainingSession.Trainer.Spoke += TrainerOnSpoke;

            _subscription = _trainingSession.Viewer.WhenPictureChanged.Subscribe(this.OnPictureChanged);
        }

        private void OnPictureChanged(Picture picture)
        {
            _dispatcher.BeginInvoke(new Action(() => { ImageLocation = picture.Filename; }));
        }

        protected override void OnViewLoaded(object view)
        {
            Task.Factory.StartNew(() => _scriptExecutor.Execute(_scriptFile)).ContinueWith(_ => OnScriptCompleted());
        }

        private void TrainerOnSpoke(object sender, SpokeEventArgs spokeEventArgs)
        {
            _dispatcher.BeginInvoke(new Action(() => { Subtitle = spokeEventArgs.Words; }));
        }

        public event EventHandler<ScriptResultEventArgs> ScriptCompleted;

        private void OnScriptCompleted()
        {
            _trainingSession.Trainer.Spoke -= TrainerOnSpoke;
            ScriptCompleted?.Invoke(this, new ScriptResultEventArgs(_scriptExecutor.Result));

            _subscription.Dispose();
            _trainingSession.Dispose();
        }
    }
}