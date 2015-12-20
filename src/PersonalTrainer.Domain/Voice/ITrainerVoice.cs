using System;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    public interface ITrainerVoice
    {
        void VoiceOff();

        void UseDefaultVoice();
        void UseVoice(string voiceHint);

        void Say(string text);
        void Say(string text, int thenPause);
        void SayAsync(string text);

        event EventHandler<SpokeEventArgs> Spoke;
    }
}