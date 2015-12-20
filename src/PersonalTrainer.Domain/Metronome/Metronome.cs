using System;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Threading;
using NLog;

namespace Figroll.PersonalTrainer.Domain.Metronome
{
    public class Metronome : IMetronome
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        private const int DefaultSpeed = 120;
        private const int MinBPM = 1;
        private const int MaxBPM = 300;

        private const string SoundFile = "53403__calaudio__wood-block.wav";
        private readonly ManualResetEvent _playStopped = new ManualResetEvent(false);
        private readonly SoundPlayer _soundPlayer = new SoundPlayer(SoundFile);

        // Roslyn and therefore scriptcs do not support async/await so have to use Timer.
        protected readonly System.Threading.Timer BeatTimer;

        private int _bpm;
        private int _count;
        private int _remaining;

        public Metronome()
        {
            BPM = DefaultSpeed;
            BeatTimer = new System.Threading.Timer(OnTick, null, Timeout.Infinite, Timeout.Infinite);
        }

        public int BPM
        {
            get { return _bpm; }
            set
            {
                if (value >= MaxBPM)
                    _bpm = MaxBPM;
                else if (value <= MinBPM)
                    _bpm = MinBPM;
                else
                    _bpm = value;
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                if (value <= 0)
                    return;

                _count = value;
                _remaining = _count;
            }
        }

        public int Remaining => _remaining;

        private bool IsPlaying()
        {
            return _playStopped.WaitOne(0);
        }

        public void PlayUntilStopped()
        {
            SetContinuousPlay();
        }

        private void SetContinuousPlay()
        {
            Count = int.MaxValue;
        }

        private bool IsContinuousPlay()
        {
            return Count == int.MaxValue;
        }

        public void Play()
        {
            Play(int.MaxValue, BPM);
        }

        public void Play(int count)
        {
            Play(count, BPM);
        }

        public void Play(int count, int bpm)
        {
            _playStopped.Reset();
            BPM = bpm;
            Count = count;

            DoPlay();
        }


        protected virtual void DoPlay()
        {
            var milliseconds = (int) (1000.0/(BPM/60.0));
            BeatTimer.Change(milliseconds, milliseconds);
        }

        protected virtual void OnTick(object state)
        {
            PlayBeat(true);
        }

        protected void PlayBeat(bool isBarEnd)
        {
            _soundPlayer.Play();

            if (IsContinuousPlay())
            {
                OnMetronomeTicked(new MetronomeEventArgs(int.MinValue, int.MinValue));
            }
            else
            {
                OnMetronomeTicked(new MetronomeEventArgs(Count - Remaining + 1, Remaining + 1));
            }

            if (isBarEnd)
            {
                EndBar();
            }
        }

        protected virtual void EndBar()
        {
            _remaining--;
            if (_remaining <= 0 && !IsContinuousPlay())
            {
                Stop();
            }
        }

        public void Stop()
        {
            BeatTimer.Change(Timeout.Infinite, Timeout.Infinite);
            _playStopped.Set();
        }

        public void WaitUntilPlayStops()
        {
            _playStopped.WaitOne();
        }

        public event EventHandler<MetronomeEventArgs> Ticked;

        private void OnMetronomeTicked(MetronomeEventArgs e)
        {
            Ticked?.Invoke(this, e);
        }
    }
}