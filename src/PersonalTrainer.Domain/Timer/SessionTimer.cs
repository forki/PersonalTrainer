using System;
using System.Threading;

namespace Figroll.PersonalTrainer.Domain.Timer
{
    public class SessionTimer: ITimer
    {
        public void WaitMillseconds(int milliseconds)
        {
            // todo convert to timer and a reset event
            Thread.Sleep(milliseconds);
        }

        public void Wait(int seconds)
        {
            // todo convert to timer and a reset event
            Thread.Sleep(seconds * 1000);
        }

        public event EventHandler<TimerEventArgs> TimerTicked;

        protected virtual void OnTimerTicked(TimerEventArgs e)
        {
            TimerTicked?.Invoke(this, e);
        }
    }
}