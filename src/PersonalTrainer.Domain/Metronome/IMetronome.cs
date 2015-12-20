using System;

namespace Figroll.PersonalTrainer.Domain.Metronome
{
    public interface IMetronome
    {
        int BPM { get; set; }
        int Count { get; set; }
        int Remaining { get;  }

        void PlayUntilStopped();

        void Play();
        void Play(int count);
        void Play(int count, int bpm);

        event EventHandler<MetronomeEventArgs> Ticked;

        void Stop();
        void WaitUntilPlayStops();
    }
}