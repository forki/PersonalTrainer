using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptCollectionViewModel : PropertyChangedBase
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private string _collectionFolderName;

        private string _collectionName;
        private ScriptViewModel _selectedScript;


        public ScriptCollectionViewModel(string collectionName, string collectionFolderName)
        {
            _collectionName = collectionName;
            _collectionFolderName = collectionFolderName;

            LoadScripts();
            SelectedScript = Scripts.FirstOrDefault();
        }

        public ObservableCollection<ScriptViewModel> Scripts { get; private set; }

        public string CollectionName
        {
            get => _collectionName;
            set
            {
                _collectionName = value;
                NotifyOfPropertyChange(() => CollectionName);
            }
        }

        public string CollectionFolderName
        {
            get => _collectionFolderName;
            set
            {
                _collectionFolderName = value;
                NotifyOfPropertyChange(() => CollectionFolderName);
            }
        }

        public ScriptViewModel SelectedScript
        {
            get => _selectedScript;
            set
            {
                _selectedScript = value;
                NotifyOfPropertyChange(() => SelectedScript);
            }
        }

        private void LoadScripts()
        {
            Scripts = new ObservableCollection<ScriptViewModel>();

            try
            {
                var directories = Directory.GetFiles(_collectionFolderName, "*.csx");
                directories.Each(f => Scripts.Add(new ScriptViewModel(Path.GetFileNameWithoutExtension(f), f)));
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Exception thrown reading " + _collectionFolderName);
            }
        }
    }
}