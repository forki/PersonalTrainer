using System;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain
{
    public class TrainingSession : ITrainingSession, IDisposable
    {
        private readonly ITaunter _taunter;
        public ITimer Timer { get; }
        public IUserSettings Settings { get; }
        public ITrainer Trainer { get; }
        public ITaunter Taunter { get; }
        public IMetronome Metronome { get; }
        public ISequencer Sequencer { get; }
        public IContentCollection Content { get; }
        public IContentViewer Viewer { get; }
        public IRandomNumberGenerator RNG { get; }

        public TrainingSession(IUserSettings settings, ITimer timer, ITrainer trainer, ITaunter taunter, IMetronome metronome, ISequencer sequencer, IContentCollection content, 
            IContentViewer viewer, IRandomNumberGenerator randomNumberGenerator)
        {
            _taunter = taunter;
            Settings = settings;
            Trainer = trainer;
            Taunter = taunter;
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
            Taunter.Stop();
        }
    }
}