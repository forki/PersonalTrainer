using Figroll.PersonalTrainer.Domain.Content;
using Figroll.PersonalTrainer.Domain.Metronome;
using Figroll.PersonalTrainer.Domain.Timer;
using Figroll.PersonalTrainer.Domain.Voice;

namespace Figroll.PersonalTrainer.Domain
{
    public class TrainingSession : ITrainingSession
    {
        public ITimer Timer { get; }
        public ITrainerVoice Trainer { get; }
        public IMetronome Metronome { get; }
        public ISequencer Sequencer { get; }
        public IContentCollection Content { get; }
        public IContentViewer Viewer { get; }
        public IRandomNumberGenerator RNG { get; }

        public TrainingSession(ITimer timer, ITrainerVoice trainer, IMetronome metronome, ISequencer sequencer, IContentCollection content, 
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
    }
}