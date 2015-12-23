using System;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer
{
    public class ScriptResultEventArgs : EventArgs
    {
        public ScriptResult Result { get; private set; }

        public ScriptResultEventArgs(ScriptResult result)
        {
            Result = result;
        }
    }
}