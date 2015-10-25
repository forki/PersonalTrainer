using System.Collections.Generic;
using ScriptCs;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HostedScriptExecutor: IHostedScriptExecutor
    {
        private readonly ScriptCsHost _script;

        public HostedScriptExecutor(ITrainingSession session)
        {
            _script = new ScriptCsHost();

            _script.Root.Executor.Initialize(new List<string>(), new List<IScriptPack>() { new PersonalTrainerScriptPack(session) });
            _script.Root.Executor.AddReferenceAndImportNamespaces(new[] { typeof(TrainingSession) });
        }

        public void Execute(string scriptFile)
        {
            _script.Root.Executor.Execute(scriptFile);
        }

        public void ExecuteScript(string script)
        {
            _script.Root.Executor.ExecuteScript(script);
        }
    }
}