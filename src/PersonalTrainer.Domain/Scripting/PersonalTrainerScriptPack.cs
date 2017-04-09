using Figroll.PersonalTrainer.Domain.API;
using ScriptCs.Contracts;

namespace Figroll.PersonalTrainer.Domain.Scripting
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PersonalTrainerScriptPack : IScriptPack
    {
        private readonly ITrainingSession _session;

        public PersonalTrainerScriptPack(ITrainingSession session)
        {
            _session = session;
        }

        public void Initialize(IScriptPackSession session)
        {
        }

        public IScriptPackContext GetContext()
        {
            return _session;
        }

        public void Terminate()
        {
            _session.Dispose();
        }
    }
}