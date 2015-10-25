using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Security.RightsManagement;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Voice;
using NLog;
using Action = System.Action;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(SessionViewModel))]
    public class SessionViewModel : Screen
    {
        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

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
            Task.Factory.StartNew(() => _scriptExecutor.ExecuteScript(_script)).ContinueWith(_ => OnScriptCompleted());
        }

        private string _script = @"
var _ = Require<TrainingSession>();
_.Trainer.UseVoice(""Amy"");
_.Trainer.SayAsync(""This is a very long speech that should be over by the time we hear the mext voice."");
_.Trainer.Say(""Hello from the script"");
";

        private void TrainerOnSpoke(object sender, SpokeEventArgs spokeEventArgs)
        {
            Subtitle = spokeEventArgs.Words;
        }

        public event EventHandler<EventArgs> ScriptCompleted;

        protected virtual void OnScriptCompleted()
        {
            ScriptCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}