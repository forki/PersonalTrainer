using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    public class ScriptCsHost
    {
        private ScriptServices CreateScriptServices()
        {
            var console = new PersonalTrainerConsole();
            var builder = new ScriptServicesBuilder(console, new DefaultLogProvider());

            builder.ScriptEngine<RoslynScriptEngine>();

            builder.FileSystem<ScriptFileSystem>();
            return builder.Build();
        }

        public ScriptServices Root { get; private set; }

        public ScriptCsHost()
        {
            Root = CreateScriptServices();
        }
    }
}