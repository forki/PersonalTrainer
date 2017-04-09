using System;
using System.Media;
using System.Reflection;
using System.Threading;
using Figroll.PersonalTrainer.Domain.API;
using NLog;

namespace Figroll.PersonalTrainer.Domain.Beats
{
    public class Metronome : IMetronome
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private const string SoundFile = "53403__calaudio__wood-block.wav";
        private const int ContinuousPlay = int.MaxValue;
        private const int DefaultSpeed = 120;
        private const int MinBPM = 1;
        private const int MaxBPM = 360;

        private readonly ManualResetEvent _playStopped = new ManualResetEvent(false);
        private readonly SoundPlayer _soundPlayer = new SoundPlayer(SoundFile);

        // Roslyn and therefore scriptcs do not support async/await so have to use Timer.
        protected readonly System.Threading.Timer BeatTimer;

        private int _bpm;
        private int _count;

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
                {
                    _logger.Debug($"Attempt to set BPM to {value} which exceeds limit of {MaxBPM}. BPM set to {value}.");
                    _bpm = MaxBPM;
                }
                else if (value <= MinBPM)
                {
                    _logger.Debug($"Attempt to set BPM to {value} which is below limit of {MinBPM}. BPM set to {value}.");
                    _bpm = MinBPM;
                }
                else
                {
                    _logger.Debug($"Metronome BPM was {_bpm} now {value}");
                    _bpm = value;
                }
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                if (value <= 0)
                {
                    return;
                }

                _count = value;
                Remaining = _count;

                _logger.Debug($"Metronome count set to {_count}");
            }
        }

        public int Remaining { get; private set; }

        public void Play()
        {
            Play(Count, BPM);
        }

        public void Play(int count)
        {
            Play(count, BPM);
        }

        public void Play(int count, int bpm)
        {
            _logger.Debug($"Metronome play {count} beats at {_bpm}");
            _playStopped.Reset();

            BPM = bpm;
            Count = count;

            PlayBeat(true);
            DoPlay();
        }

        public void Start()
        {
            _logger.Debug($"Continuous play started at speed {BPM}");
            Play(ContinuousPlay, BPM);
        }

        public void WaitUntilPlayStops()
        {
            _logger.Debug($"Waiting until metronome stops.");
            _playStopped.WaitOne();
        }

        public void Stop()
        {
            _logger.Debug($"Metronome stopped.");
            BeatTimer.Change(Timeout.Infinite, Timeout.Infinite);
            _playStopped.Set();
        }

        public event EventHandler<MetronomeEventArgs> Ticked;

        // ReSharper disable once UnusedMember.Local
        private bool IsPlaying() => _playStopped.WaitOne(0);
        private bool IsContinuousPlay() => Count == ContinuousPlay;

        protected virtual void DoPlay()
        {
            var beatIntervalMilliseconds = (int) (1000.0 / (BPM / 60.0));
            BeatTimer.Change(beatIntervalMilliseconds, beatIntervalMilliseconds);
        }

        protected virtual void OnTick(object state)
        {
            PlayBeat(true);
        }

        protected void PlayBeat(bool isBarEnd)
        {
            _logger.Trace($"Playing beat bar end = {isBarEnd}");
            _soundPlayer.Play();

            OnMetronomeTicked(IsContinuousPlay()
                ? new MetronomeEventArgs(int.MinValue, int.MinValue)
                : new MetronomeEventArgs(Count - Remaining + 1, Remaining + 1));

            if (isBarEnd)
            {
                EndBar();
            }
        }

        protected void EndBar()
        {
            if (IsContinuousPlay())
            {
                return;
            }

            if (Remaining-- <= 1)
            {
                Stop();
            }
        }

        private void OnMetronomeTicked(MetronomeEventArgs e)
        {
            Ticked?.Invoke(this, e);
        }
    }
}