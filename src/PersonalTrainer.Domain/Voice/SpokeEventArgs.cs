using System;

namespace Figroll.PersonalTrainer.Domain.Voice
{
    public class SpokeEventArgs : EventArgs
    {
        public string Words { get; private set; }

        public SpokeEventArgs(string words)
        {
            Words = words;
        }
    }
}