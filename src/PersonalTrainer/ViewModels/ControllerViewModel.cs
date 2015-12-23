using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Scripting;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(ControllerViewModel))]
    public class ControllerViewModel: Screen
    {
        private readonly Logger _logger = NLog.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        //private SessionViewModel _session;

        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;

        private string _backgroundImage = string.Empty;

        private ScriptViewModel _selectedScript;
        public ObservableCollection<ScriptViewModel> Scripts { get; private set; }

        public string BackgroundImage
        {
            get { return _backgroundImage; }
            private set
            {
                _backgroundImage = value;
                NotifyOfPropertyChange(() => BackgroundImage);
            }
        }

        public ScriptViewModel SelectedScript
        {
            get { return _selectedScript; }
            set
            {
                _selectedScript = value;
                NotifyOfPropertyChange(() => SelectedScript);
            }
        }

        public ControllerViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;

            BackgroundImage = @"../Resources/autotrainer.jpg";

            LoadScripts();
            SelectedScript = Scripts.FirstOrDefault();
        }


        private void LoadScripts()
        {
            var scriptDirectory = string.Empty;
            Scripts = new ObservableCollection<ScriptViewModel>();

            try
            {
                scriptDirectory = @"./Scripts";

                var files = Directory.GetFiles(scriptDirectory, "*.csx");
                files.ForEach(f => Scripts.Add(new ScriptViewModel(Path.GetFileNameWithoutExtension(f), f)));
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Exception thrown reading " + scriptDirectory);
            }
        }

   }
}
