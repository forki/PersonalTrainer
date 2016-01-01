using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
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

        private readonly ITrainingSession _trainingSession;
        private readonly IHostedScriptExecutor _scriptExecutor;

        private string _backgroundImage = string.Empty;

        private ScriptCollectionViewModel _selectedCollection;
        public ObservableCollection<ScriptCollectionViewModel> ScriptCollections { get; private set; }

        public string BackgroundImage
        {
            get { return _backgroundImage; }
            private set
            {
                _backgroundImage = value;
                NotifyOfPropertyChange(() => BackgroundImage);
            }
        }

        public ScriptCollectionViewModel SelectedCollection
        {
            get { return _selectedCollection; }
            set
            {
                _selectedCollection = value;
                NotifyOfPropertyChange(() => SelectedCollection);
            }
        }

        public ControllerViewModel(ITrainingSession trainingSession, IHostedScriptExecutor scriptExecutor)
        {
            _trainingSession = trainingSession;
            _scriptExecutor = scriptExecutor;

            BackgroundImage = @"../Resources/autotrainer.jpg";

            LoadScripts();
            SelectedCollection = ScriptCollections.FirstOrDefault();
        }


        private void LoadScripts()
        {
            // todo put this in settings
            var scriptDirectory = @"./content";
            ScriptCollections = new ObservableCollection<ScriptCollectionViewModel>();

            try
            {
                var directories = Directory.GetDirectories(scriptDirectory);
                directories.ForEach(f => ScriptCollections.Add(new ScriptCollectionViewModel(Path.GetFileNameWithoutExtension(f), f)));
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Exception thrown reading " + scriptDirectory);
            }
        }
   }
}
