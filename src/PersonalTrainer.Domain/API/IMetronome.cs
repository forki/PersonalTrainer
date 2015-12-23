using System;
using Figroll.PersonalTrainer.Domain.Beats;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IMetronome
    {
        int BPM { get; set; }
        int Count { get; set; }
        int Remaining { get;  }

        void Play();
        void Play(int count);
        void Play(int count, int bpm);
        void PlayUntilStopped();
        void WaitUntilPlayStops();
        void Stop();

        event EventHandler<MetronomeEventArgs> Ticked;
    }
}