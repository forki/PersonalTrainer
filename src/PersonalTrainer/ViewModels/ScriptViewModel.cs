using Caliburn.Micro;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptViewModel: PropertyChangedBase
    {
        private string _scriptName;
        private string _scriptFileName;

        public string ScriptName
        {
            get { return _scriptName; }
            set
            {
                _scriptName = value;
                NotifyOfPropertyChange(() => ScriptName);
            }
        }

        public string ScriptFileName
        {
            get { return _scriptFileName; }
            set
            {
                _scriptFileName = value;
                NotifyOfPropertyChange(() => ScriptFileName);
            }
        }

        public ScriptViewModel(string scriptName, string scriptFileName)
        {
            _scriptName = scriptName;
            _scriptFileName = scriptFileName;
        }
    }
}