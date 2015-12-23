using System;
using Figroll.PersonalTrainer.Domain.Voice;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface ITrainer
    {
        void UseDefaultVoice();
        void UseVoice(string voiceHint);
        void VoiceOff();

        void Say(string text);
        void Say(string text, int thenPause);
        void Say(int pauseThen, string text, int thenPause = 0);
        void SayAsync(string text);

        event EventHandler<SpokeEventArgs> Spoke;
    }
}