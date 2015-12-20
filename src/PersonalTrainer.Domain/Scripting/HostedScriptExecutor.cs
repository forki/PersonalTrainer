using System;
using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;
using ScriptCs;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HostedScriptExecutor : IHostedScriptExecutor
    {
        private readonly ScriptCsHost _script;

        public HostedScriptExecutor(ITrainingSession session)
        {
            _script = new ScriptCsHost();

            _script.Root.Executor.Initialize(new List<string>(), new List<IScriptPack>() {new PersonalTrainerScriptPack(session)});
            _script.Root.Executor.AddReferenceAndImportNamespaces(new[]
            {
                typeof (TrainingSession),
                typeof (Metronome.Metronome),
                typeof (Metronome.Sequencer),
                typeof (Voice.TrainerVoice),
                typeof (Timer.SessionTimer),
                typeof (Picture),
                typeof (IContentViewer),
                typeof (IContentCollection),
                typeof (PictureInfo)
            });
        }

        public ScriptResult Result { get; private set; }

        public void Execute(string scriptFile)
        {
            Result = _script.Root.Executor.Execute(scriptFile);
        }

        public void ExecuteScript(string script)
        {
            Result = _script.Root.Executor.ExecuteScript(script);
        }
    }
}