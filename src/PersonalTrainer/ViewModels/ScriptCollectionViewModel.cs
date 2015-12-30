using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptCollectionViewModel : PropertyChangedBase
    {
        private readonly Logger _logger = NLog.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private string _collectionName;
        private string _collectionFolderName;
        private ScriptViewModel _selectedScript;

        public ObservableCollection<ScriptViewModel> Scripts { get; private set; }

        public string CollectionName
        {
            get { return _collectionName; }
            set
            {
                _collectionName = value;
                NotifyOfPropertyChange(() => CollectionName);
            }
        }

        public string CollectionFolderName
        {
            get { return _collectionFolderName; }
            set
            {
                _collectionFolderName = value;
                NotifyOfPropertyChange(() => CollectionFolderName);
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


        public ScriptCollectionViewModel(string collectionName, string collectionFolderName)
        {
            _collectionName = collectionName;
            _collectionFolderName = collectionFolderName;

            LoadScripts();
            SelectedScript = Scripts.FirstOrDefault();
        }

        private void LoadScripts()
        {
            Scripts = new ObservableCollection<ScriptViewModel>();

            try
            {
                var directories = Directory.GetFiles(_collectionFolderName, "*.csx");
                directories.ForEach(f => Scripts.Add(new ScriptViewModel(Path.GetFileNameWithoutExtension(f), f)));
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Exception thrown reading " + _collectionFolderName);
            }
        }

    }
}