using System;

namespace Figroll.PersonalTrainer.Domain.Timer
{
    public interface ITimer
    {
        void WaitMillseconds(int milliseconds);
        void Wait(int seconds);

        event EventHandler<TimerEventArgs> TimerTicked;
    }
}