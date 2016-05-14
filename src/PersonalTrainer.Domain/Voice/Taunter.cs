using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Taunter : Voice, ITaunter
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private readonly List<string> _neutral = new List<string>();
        private readonly List<string> _taunts = new List<string>();
        private readonly List<string> _encouragements = new List<string>();

        private IEnumerable<string> _all;

        private System.Threading.Timer _timer;

        private void DoLoad(string filename, List<string> store)
        {
            try
            {
                var fullpath = Path.GetFullPath(filename);
                store.AddRange(File.ReadAllLines(fullpath));
                _all = _taunts.Union(_encouragements).Union(_neutral);
            }
            catch (Exception e)
            {
                _logger.Error($"Could not load taunts from {filename}.");
            }
        }

        public void Load(string filename)
        {
            DoLoad(filename, _neutral);
        }

        public void Load(string filename, SpeechClassification classification)
        {
            switch (classification)
            {
                case SpeechClassification.Neutral:
                    DoLoad(filename, _neutral);
                    break;
                case SpeechClassification.Taunt:
                    DoLoad(filename, _taunts);
                    break;
                case SpeechClassification.Encourage:
                    DoLoad(filename, _encouragements);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(classification), classification, null);
            }
            
        }

        public void Taunt(SpeechClassification classification = SpeechClassification.Taunt)
        {
            switch (classification)
            {
                case SpeechClassification.Neutral:
                    DoTauntAsync(_neutral);
                    break;
                case SpeechClassification.Taunt:
                    DoTauntAsync(_taunts);
                    break;
                case SpeechClassification.Encourage:
                    DoTauntAsync(_encouragements);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(classification), classification, null);
            }
        }

        public void TauntSync(SpeechClassification classification = SpeechClassification.Taunt)
        {
            switch (classification)
            {
                case SpeechClassification.Neutral:
                    DoTaunt(_neutral);
                    break;
                case SpeechClassification.Taunt:
                    DoTaunt(_taunts);
                    break;
                case SpeechClassification.Encourage:
                    DoTaunt(_encouragements);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(classification), classification, null);
            }
        }

        public void TauntAll()
        {
            DoTaunt(_all);
        }

        private void DoTaunt(IEnumerable<string> collection)
        {
            Speak(collection.Random());
        }

        private void DoTauntAsync(IEnumerable<string> collection)
        {
            SpeakAsync(collection.Random());
        }

        public void AutoTaunt(int seconds, SpeechClassification classification = SpeechClassification.Taunt)
        {
            _timer = new System.Threading.Timer(OnTick, classification, 0, seconds.ToMilliseconds());
        }

        public void Stop()
        {
            if (_timer == null) return;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer.Dispose();
            _timer = null;
        }

        private void OnTick(object state)
        {
            Taunt((SpeechClassification) state);
        }
    }
}