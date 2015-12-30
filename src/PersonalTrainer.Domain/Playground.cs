using System.Linq;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain
{
    public class Playground
    {
        private readonly ITrainingSession _ = null;

        public void Example_1()
        {
            _.Content.Load("Collection");

            var gallery = _.Content.Gallery("Petra V");
            _.Viewer.PlaySlideshow(gallery, 4);

            _.Metronome.BPM = 60;
            _.Metronome.Start();
        }

        public void Example_2()
        {
            _.Content.Load("Collection");
            _.Trainer.UseVoice("Amy");

            var picture = _.Content.Pictures.First();
            _.Viewer.Display(picture);

            picture = _.Content.Pictures.Single(p => p.Name == "Blue Socks.jpg");
            _.Viewer.Display(picture);

            _.Metronome.WaitUntilPlayStops();
            _.Timer.Wait(3);
        }

        public void Example_3()
        {
            _.Content.Load(@"D:\Milovana\OT London");

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