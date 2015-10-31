using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Voice;
using ScriptCs.Contracts;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptResultEventArgs : EventArgs
    {
        public ScriptResult Result { get; private set; }

        public ScriptResultEventArgs(ScriptResult result)
        {
            Result = result;
        }
    }

    [Export(typeof(SessionViewModel))]
    public sealed class SessionViewModel : Screen
    {
        private readonly NLog.Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;

        private string _imageLocation = string.Empty;
        private string _subtitle;

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

        public SessionViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;
            _trainingSession.Trainer.Spoke += TrainerOnSpoke;

            ImageLocation = @"D:\Milovana\Captions\sissydru4u 1-5\207507707.jpg";
        }

        protected override void OnViewLoaded(object view)
        {
            Task.Factory.StartNew(() => _scriptExecutor.Execute(DefaultScript)).ContinueWith(_ => OnScriptCompleted());
        }

        private const string DefaultScript = "./scripts/default.csx";

        private void TrainerOnSpoke(object sender, SpokeEventArgs spokeEventArgs)
        {
            Subtitle = spokeEventArgs.Words;
        }

        public event EventHandler<ScriptResultEventArgs> ScriptCompleted;

        private void OnScriptCompleted()
        {
            _trainingSession.Trainer.Spoke -= TrainerOnSpoke;
            ScriptCompleted?.Invoke(this, new ScriptResultEventArgs(_scriptExecutor.Result));
        }
    }
}