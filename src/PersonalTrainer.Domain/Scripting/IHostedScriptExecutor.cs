namespace Figroll.PersonalTrainer.Domain.Scripting
{
    public interface IHostedScriptExecutor
    {
        void Execute(string scriptFile);
        void ExecuteScript(string script);
    }
}