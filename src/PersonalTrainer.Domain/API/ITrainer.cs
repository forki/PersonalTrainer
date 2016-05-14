using System;
using Figroll.PersonalTrainer.Domain.Voice;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IVoice
    {
        void UseDefaultVoice();
        void UseVoice(string voiceHint);
        void VoiceOff();

        event EventHandler<SpokeEventArgs> Spoke;
    }

    public interface ITrainer: IVoice
    {
        void Say(string text);
        void Say(string text, int thenPause);
        void SayAsync(string text);
    }

    public interface ITaunter : IVoice
    {
        void Load(string filename);
        void Load(string filename, SpeechClassification classification);

        void TauntAll();
        void Taunt(SpeechClassification classification = SpeechClassification.Taunt);
        void TauntSync(SpeechClassification classification = SpeechClassification.Taunt);
        void AutoTaunt(int seconds, SpeechClassification classification = SpeechClassification.Taunt);
        void Stop();
    }
}