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
    public class Voice : IVoice
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private const int TrainerSpeechEndPause = 500;
        private const int MilisecondsPerWord = 400;

        private ReadOnlyCollection<InstalledVoice> _installedVoices;
        protected readonly SpeechSynthesizer Speaker = new SpeechSynthesizer();
        protected bool VoiceSelected;

        public void VoiceOff()
        {
            _logger.Trace("TTS disabled.");
            VoiceSelected = false;
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
                VoiceSelected = false;
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
            VoiceSelected = true;
        }

        public virtual event EventHandler<SpokeEventArgs> Spoke;
        protected void OnSpoke(SpokeEventArgs e)
        {
            Spoke?.Invoke(this, e);
        }

        private void GetInstalledVoices()
        {
            _installedVoices = Speaker.GetInstalledVoices();
        }

        private static bool HaveVoiceHint(string voiceHint)
        {
            return voiceHint != string.Empty;
        }

        private void SelectRandomFavouredVoice(string favourVoiceHint)
        {
            var enabledVoicesMatchingHint = Enumerable.ToArray<InstalledVoice>(GetEnabledVoicesMatchingHint(favourVoiceHint));

            if (!enabledVoicesMatchingHint.Any())
                VoiceSelected = false;

            SelectRandom(enabledVoicesMatchingHint);

            VoiceSelected = true;
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
            Speaker.SelectVoice(voice);
        }

        private void SelectRandomAdultWoman()
        {
            Speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
        }

        private IEnumerable<InstalledVoice> GetEnabledVoicesMatchingHint(string favourVoiceHint)
        {
            return _installedVoices.Where(v => v.VoiceInfo.Name.Contains(favourVoiceHint) && v.Enabled);
        }

        private void AutoClearSpeechTrackingOnComplete()
        {
            Speaker.StateChanged += (sender, args) =>
            {
                if (args.PreviousState != SynthesizerState.Speaking || args.State != SynthesizerState.Ready) return;
                OnSpoke(new SpokeEventArgs(string.Empty));
            };
        }

        private bool NoVoiceSelected()
        {
            return VoiceSelected == false;
        }

        protected void Speak(string text)
        {
            if (VoiceSelected)
            {
                OnSpoke(new SpokeEventArgs(text));
                Speaker.Speak(text);
            }
            else
            {
                SubtitleOnlySay(text);
            }
        }

        protected void SpeakAsync(string text)
        {
            if (VoiceSelected)
            {
                OnSpoke(new SpokeEventArgs(text));
                Speaker.SpeakAsync(text);
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
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Trainer : Voice, ITrainer
    {
        public void Say(string text)
        {
            Speak(text);
        }

        public void Say(string text, int thenPause)
        {
            Say(text);
            Thread.Sleep(thenPause.ToMilliseconds());
        }

        public void SayAsync(string text)
        {
            SpeakAsync(text);
        }
    }
}