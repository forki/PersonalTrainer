using System;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    public interface ITrainerVoice
    {
        void VoiceOff();

        void UseDefaultVoice();
        void UseVoice(string voiceHint);

        void Say(string text);
        void SayAsync(string text);

        event EventHandler<SpokeEventArgs> Spoke;
    }
}