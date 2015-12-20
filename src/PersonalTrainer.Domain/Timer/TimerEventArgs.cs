using System;

namespace Figroll.PersonalTrainer.Domain.Timer
{
    public class TimerEventArgs: EventArgs
    {
        TimeSpan Time { get; }    
        TimeSpan TimeRemaining { get; }

        public TimerEventArgs(TimeSpan time, TimeSpan timeRemaining)
        {
            Time = time;
            TimeRemaining = timeRemaining;
        }
    }
}