using System;
using System.Runtime.CompilerServices;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    public enum SpeechClassification
    {
        Neutral,
        Taunt,
        Encourage
    }

    public class SpokeEventArgs : EventArgs
    {
        public SpeechClassification Classification { get; private set; }
        public string Words { get; private set; }

        public SpokeEventArgs(string words, SpeechClassification classification = SpeechClassification.Neutral)
        {
            Classification = classification;
            Words = words;
        }
    }
}