using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;
using LogManager = NLog.LogManager;

namespace Figroll.PersonalTrainer.ViewModels
{
    public class ScriptViewModel : PropertyChangedBase
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private string _scriptDescription;
        private string _scriptFileName;
        private string _scriptName;

        public ScriptViewModel(string scriptName, string scriptFileName)
        {
            _scriptName = scriptName;
            _scriptFileName = scriptFileName;

            try
            {
                File.ReadAllLines(scriptFileName)
                    .SkipWhile(line => line.StartsWith("#load") || string.IsNullOrWhiteSpace(line))
                    .TakeWhile(line => line.StartsWith("//"))
                    .Each(x => { _scriptDescription += x.Remove(0, 2).Trim() + Environment.NewLine; });

                if (!string.IsNullOrEmpty(_scriptDescription))
                    _scriptDescription = _scriptDescription.Remove(_scriptDescription.Length - 2, 2);
            }
            catch (Exception e)
            {
                _scriptDescription = string.Empty;
                _logger.Fatal(e, "Exception thrown reading " + scriptFileName);
            }
        }

        public string ScriptName
        {
            get => _scriptName;
            set
            {
                _scriptName = value;
                NotifyOfPropertyChange(() => ScriptName);
            }
        }

        public string ScriptFileName
        {
            get => _scriptFileName;
            set
            {
                _scriptFileName = value;
                NotifyOfPropertyChange(() => ScriptFileName);
            }
        }

        public string ScriptDescription
        {
            get => _scriptDescription;
            set
            {
                _scriptDescription = value;
                NotifyOfPropertyChange(() => ScriptDescription);
            }
        }
    }
}