using System;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain
{
    public class TrainingSession : ITrainingSession, IDisposable
    {
        public ITimer Timer { get; }
        public ITrainer Trainer { get; }
        public IMetronome Metronome { get; }
        public ISequencer Sequencer { get; }
        public IContentCollection Content { get; }
        public IContentViewer Viewer { get; }
        public IRandomNumberGenerator RNG { get; }

        public TrainingSession(ITimer timer, ITrainer trainer, IMetronome metronome, ISequencer sequencer, IContentCollection content, 
            IContentViewer viewer, IRandomNumberGenerator randomNumberGenerator)
        {
            Trainer = trainer;
            Metronome = metronome;
            Sequencer = sequencer;
            Content = content;
            Viewer = viewer;
            Timer = timer;
            RNG = randomNumberGenerator;
        }

        public void Dispose()
        {
            Metronome.Stop();
            Sequencer.Stop();
        }
    }
}