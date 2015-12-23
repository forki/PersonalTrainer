using System.Linq;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain
{
    public class TestBed
    {
        private ITrainingSession _;

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

            var picture = _.Content.Pictures.First();
            _.Viewer.Display(picture);

            picture = _.Content.Pictures.Single(p => p.Name == "Blue Socks.jpg");
            _.Viewer.Display(picture);

            _.Timer.Wait(3);
        }

        public void Example_3()
        {
            _.Content.Load("Collection");

            var picture = _.Content.GetPicture("blue socks");

            int bpm = 60;
            int beats = 10;

            for (int i = 0; i < 3; i++)
            {
                _.Viewer.Display(picture);

                //StrokePlease(bpm, beats);

                // We can clear the screen like this:
                _.Viewer.Clear();

                // And give the player a short 3 second rest like this:
                _.Timer.Wait(3);
            }
        }
    }
}