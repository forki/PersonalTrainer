using System;
using System.Threading;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain.Timer
{
    public class SessionTimer: ITimer
    {
        public void WaitMilliseconds(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        private static System.Threading.Timer _timer;

        public void RunAsync(int seconds)
        {
            if (_timer != null) return;

            _timer = new System.Threading.Timer(OnComplete, null, seconds.ToMilliseconds(), Timeout.Infinite);
        }

        private void OnComplete(object state)
        {
            if (_timer == null) return;

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer.Dispose();
            _timer = null;

            OnTimerEnded();
        }

        public event EventHandler<TimerEventArgs> TimerTicked;

        protected virtual void OnTimerTicked(TimerEventArgs e)
        {
            TimerTicked?.Invoke(this, e);
        }

        public event EventHandler<EventArgs> TimerEnded;

        protected virtual void OnTimerEnded()
        {
            TimerEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}