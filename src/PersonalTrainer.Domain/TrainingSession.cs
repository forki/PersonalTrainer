using Figroll.PersonalTrainer.Domain.Voice;

namespace Figroll.PersonalTrainer.Domain
{
    public class TrainingSession : ITrainingSession
    {
        public ITrainerVoice Trainer { get; private set; }

        public TrainingSession(ITrainerVoice trainer)
        {
            Trainer = trainer;
        }
    }
}