using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain
{
    /// <summary>
    ///     This class just exists for hacking about with bits of script.
    /// </summary>
    public class Playground
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly ITrainingSession _;

        public Playground(ITrainingSession x)
        {
            _ = x;
        }

        public void Example_1()
        {
            // Can hack scripts here with IDE and compiler support.
        }
    }
}