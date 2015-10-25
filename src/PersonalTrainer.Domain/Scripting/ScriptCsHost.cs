using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    public class ScriptCsHost
    {
        private ScriptServices CreateScriptServices(bool useLogging)
        {
            var console = new ScriptConsole();
            var builder = new ScriptServicesBuilder(console, new DefaultLogProvider());

            builder.ScriptEngine<RoslynScriptEngine>();

            builder.FileSystem<ScriptFileSystem>();
            return builder.Build();
        }

        public ScriptServices Root { get; private set; }

        public ScriptCsHost()
        {
            Root = CreateScriptServices(false);
        }
    }
}