using System;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface ITrainingSession: IScriptPackContext, IDisposable
    {
        ITrainer Trainer { get; }

        IRandomNumberGenerator RNG { get; }
        ITimer Timer { get; }

        IMetronome Metronome { get; }
        ISequencer Sequencer { get; }

        IContentCollection Content { get; }
        IContentViewer Viewer { get; }
    }
}