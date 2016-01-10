using System.Linq;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain
{
    /// <summary>
    /// This class just exists for hacking about with bits of script.
    /// </summary>
    public class Playground
    {
        private readonly ITrainingSession _ = null;

        public Playground(ITrainingSession x)
        {
            _ = x;
        }

        public void Example_1()
        {
            var yes = 0;
            var no = 0;

            for (int i = 1000 - 1; i >= 0; i--)
            {
                if (_.RNG.IsPercentageChance(90))
                {
                    yes++;
                }
                else
                {
                    no++;
                }
            }
        }

        public void Example_2()
        {
        }

        public void Example_3()
        {
            _.Content.Load(@"D:\Milovana\");

            var shuffledGalleries = _.Content.Galleries.Shuffle();

            int n = 0;
            foreach (var picture in shuffledGalleries.SelectMany(gallery => gallery.Pictures))
            {
                _.Viewer.Display(picture, 1);
                _.Metronome.Play(10, 120);
                _.Trainer.Say("Next picture.");

                if (n++ == 10)
                {
                    _.Trainer.Say("Rest now.");
                    _.Timer.Wait(_.RNG.Between(5, 15));
                }
            }
       }
    }
}