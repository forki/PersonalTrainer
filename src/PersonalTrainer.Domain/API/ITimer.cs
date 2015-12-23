using System;
using Figroll.PersonalTrainer.Domain.Timer;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface ITimer
    {
        void Wait(int seconds);
        void WaitMillseconds(int milliseconds);

        event EventHandler<TimerEventArgs> TimerTicked;
    }
}