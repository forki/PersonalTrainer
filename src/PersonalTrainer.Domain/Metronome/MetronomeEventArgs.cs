using System;

namespace Figroll.PersonalTrainer.Domain.Metronome
{
    public class MetronomeEventArgs : EventArgs
    {
        public int TicksDone { get; }
        public int TicksRemaining { get; }

        public MetronomeEventArgs(int ticksDone, int ticksRemaining)
        {
            TicksDone = ticksDone;
            TicksRemaining = ticksRemaining;
        }
    }
}