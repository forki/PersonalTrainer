using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;
using NLog;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Trainer : ITrainer
    {
        private const int TrainerSpeechEndPause = 500;
        private const int MilisecondsPerWord = 400;

        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());
        private readonly SpeechSynthesizer _speaker = new SpeechSynthesizer();
        private ReadOnlyCollection<InstalledVoice> _installedVoices;
        private bool _voiceSelected;

        public void VoiceOff()
        {
            _logger.Trace("TTS disabled.");
            _voiceSelected = false;
        }

        public void UseDefaultVoice()
        {
            _logger.Trace("Selecting default voice");
            UseVoice(string.Empty);
        }

        public void UseVoice(string voiceHint)
        {
            GetInstalledVoices();

            if (!_installedVoices.Any())
            {
                _logger.Error("Could not find any installed TTS voices.  TTS will be disabled.");
                _voiceSelected = false;
                return;
            }

            if (HaveVoiceHint(voiceHint))
            {
                _logger.Trace("Selecing voice from hint " + voiceHint);
                SelectRandomFavouredVoice(voiceHint);
            }

            if (NoVoiceSelected())
            {
                _logger.Trace("No voice selected will use random voice");
                SmartSelectRandomVoice();
            }

            AutoClearSpeechTrackingOnComplete();
            _voiceSelected = true;
        }

        private void GetInstalledVoices()
        {
            _installedVoices = _speaker.GetInstalledVoices();
        }

        private static bool HaveVoiceHint(string voiceHint)
        {
            return voiceHint != string.Empty;
        }

        private void SelectRandomFavouredVoice(string favourVoiceHint)
        {
            var enabledVoicesMatchingHint = GetEnabledVoicesMatchingHint(favourVoiceHint).ToArray();

            if (!enabledVoicesMatchingHint.Any())
                _voiceSelected = false;

            SelectRandom(enabledVoicesMatchingHint);

            _voiceSelected = true;
        }

        private void SmartSelectRandomVoice()
        {
            // Prefer non-MSFT voices as they are more likely to be high quality paid for voices.
            var nonMicrosoftVoicestVoices = _installedVoices.Where(v => !v.VoiceInfo.Name.Contains("Microsoft") &&
                                                                        v.VoiceInfo.Gender == VoiceGender.Female &&
                                                                        v.VoiceInfo.Age != VoiceAge.Child && v.Enabled).ToList();

            if (nonMicrosoftVoicestVoices.Any())
            {
                SelectRandom(nonMicrosoftVoicestVoices);
            }
            else
            {
                SelectRandomAdultWoman();
            }
        }

        private void SelectRandom(IEnumerable<InstalledVoice> userInstalledVoices)
        {
            var voice = userInstalledVoices.Random().VoiceInfo.Name;
            _speaker.SelectVoice(voice);
        }

        private void SelectRandomAdultWoman()
        {
            _speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
        }


        private IEnumerable<InstalledVoice> GetEnabledVoicesMatchingHint(string favourVoiceHint)
        {
            return _installedVoices.Where(v => v.VoiceInfo.Name.Contains(favourVoiceHint) && v.Enabled);
        }

        private void AutoClearSpeechTrackingOnComplete()
        {
            _speaker.StateChanged += (sender, args) =>
            {
                if (args.PreviousState != SynthesizerState.Speaking || args.State != SynthesizerState.Ready) return;
                OnSpoke(new SpokeEventArgs(string.Empty));
            };
        }

        private bool NoVoiceSelected()
        {
            return _voiceSelected == false;
        }

        public void Say(string text)
        {
            if (_voiceSelected)
            {
                OnSpoke(new SpokeEventArgs(text));
                _speaker.Speak(text);
            }
            else
            {
                SubtitleOnlySay(text);
            }
        }

        public void Say(string text, int thenPause)
        {
            Say(text);
            Thread.Sleep(thenPause.ToMilliseconds());
        }

        public void Say(int pauseThen, string text)
        {
            Thread.Sleep(pauseThen.ToMilliseconds());
            Say(text);
        }

        public void SayAsync(string text)
        {
            if (_voiceSelected)
            {
                OnSpoke(new SpokeEventArgs(text));
                _speaker.SpeakAsync(text);
            }
            else
            {
                Task.Factory.StartNew(() => SubtitleOnlySay(text));
            }
        }

        private void SubtitleOnlySay(string words)
        {
            OnSpoke(new SpokeEventArgs(words));

            var spaceCount = words.Count(Char.IsWhiteSpace);
            var delay = (spaceCount + 1) * MilisecondsPerWord + TrainerSpeechEndPause;
            Thread.Sleep(delay);

            OnSpoke(new SpokeEventArgs(string.Empty));
        }

        public event EventHandler<SpokeEventArgs> Spoke;

        private void OnSpoke(SpokeEventArgs e)
        {
            Spoke?.Invoke(this, e);
        }
    }
}