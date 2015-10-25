using System;
using ScriptCs;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    public class ScriptFileSystem : FileSystem
    {
        public override string BinFolder => AppDomain.CurrentDomain.BaseDirectory;
    }
}