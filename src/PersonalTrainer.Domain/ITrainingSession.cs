using Figroll.PersonalTrainer.Domain.Content;
using Figroll.PersonalTrainer.Domain.Metronome;
using Figroll.PersonalTrainer.Domain.Timer;
using Figroll.PersonalTrainer.Domain.Voice;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain
{
    public interface ITrainingSession: IScriptPackContext
    {
        ITrainerVoice Trainer { get; }

        IRandomNumberGenerator RNG { get; }
        ITimer Timer { get; }

        IMetronome Metronome { get; }
        ISequencer Sequencer { get; }

        IContentCollection Content { get; }
        IContentViewer Viewer { get; }
    }
}