using Figroll.PersonalTrainer.Domain.Voice;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain
{
    public interface ITrainingSession: IScriptPackContext
    {
        ITrainerVoice Trainer { get; }
    }
}