using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Autofac.Core;
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

        private enum Speed
        {
            Slow,
            Steady,
            Normal,
            Fast,
            Super,
        }

        private readonly string[] Templates = new[]
        {
            "For this picture {0} {1} strokes please.",
            "{0} {1}",
            "Stroke {1} for {0} strokes.",
            "Keep going. {0} {1} please.",
            "Look at her and give me {0} {1} please.",
            "{0} {1} while staring at that beautiful body.",
        };

        private readonly string[] UncommonTemplates = new[]
        {
            "You can do this. Work it baby {0} {1} please.",
            "You are probably near to coming but you are not allowed to come. Can you do {0} {1}?",
            "Can you see her tits yet?  If you can do extra hard strokes this time.  {0} {1}",
            "Work for her pussy.  {0} {1} and you will get to enjoy that sight.",
        };

        private readonly Dictionary<Speed, int> tempos = new Dictionary<Speed, int>()
        {
            { Speed.Slow, 60},
            { Speed.Steady, 120},
            { Speed.Normal, 120},
            { Speed.Fast, 140},
            { Speed.Super, 180},
        };

        private void Task(int count, Speed speed)
        {
            var templateSet = Templates;

            if (_.RNG.IsPercentageChance(25))
            {
                templateSet = UncommonTemplates;
            }

            var instruction = string.Format(templateSet.Random(), count, speed);
            _.Trainer.Say(instruction);

            _.Metronome.Play(count, tempos[speed]);
        }

        public void Example_2()
        {
            int reps = 0;
            while (true)
            {
                int n = _.RNG.Between(1, 10);

                var speed = Speed.Normal;
                if (_.RNG.IsPercentageChance(15)) speed = Speed.Slow;
                else if (_.RNG.IsPercentageChance(25)) speed = Speed.Steady;
                else if (_.RNG.IsPercentageChance(25)) speed = Speed.Fast;
                else if (_.RNG.IsPercentageChance(10)) speed = Speed.Super;

                for (int i = 0; i < n; i++)
                {
                    var countMin = 3;
                    var countMax = 10;
                    var step = 1;

                    if (reps > 10)
                    {
                        countMin = 5;
                        countMax = 20;
                        step = 5;
                    }
                    else if (reps > 20)
                    {
                        countMin = 10;
                        countMax = 30;
                        step = 10;
                    }

                    Task(_.RNG.Between(countMin, countMax, step), speed);
                    reps++;

                    if (reps%10 == 0)
                    {
                        _.Trainer.Say("Take a short rest");
                        _.Timer.Wait(_.RNG.Between(5, 15));
                    }
                    else if (_.RNG.IsPercentageChance(10) && reps%10 >= 3)
                    {
                        _.Trainer.Say("Take a short rest");
                        _.Timer.Wait(_.RNG.Between(10, 20));
                    }

                    _.Trainer.Say("Next picture");
                }
            }
        }

        public void Example_3()
        {
            _.Content.Load(@"D:\Milovana\");

            var shuffledGalleries = _.Content.Galleries.Shuffle();

            var test = shuffledGalleries.First().Pictures;
            int step = test.Count() / 10;
            var selection = test.Where((x, i) => i%step == 0);

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