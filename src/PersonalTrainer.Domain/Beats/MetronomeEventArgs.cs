using System;

namespace Figroll.PersonalTrainer.Domain.Beats
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