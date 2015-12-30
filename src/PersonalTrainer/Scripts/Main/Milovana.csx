var _ = Require<TrainingSession>();

_.Content.Load(@"Z:\Junipur-1");
_.Trainer.UseVoice("Amy");

var shuffledGalleries = _.Content.Galleries.Shuffle();

int n = 0;
foreach (var picture in _.Content.Pictures)
{
    _.Viewer.Display(picture);

    if (_.RNG.IsPercentageChance(25))
    {
        _.Trainer.Say("Test him.");
        _.Metronome.Play(_.RNG.Between(30, 60), 180);
    }
    else if (_.RNG.IsPercentageChance(25))
    {
        _.Trainer.Say("Relentless.");
        _.Metronome.Play(_.RNG.Between(20, 40), 120);
    }
    else
    {
        _.Trainer.Say("Stroke.");
        _.Metronome.Play(_.RNG.Between(10, 20), _.RNG.Between(80, 120));
    }

    _.Metronome.WaitUntilPlayStops();
    _.Trainer.Say("Stop.");
    _.Timer.Wait(_.RNG.Between(1, 3));

    if (n++ == 5)
    {
        _.Trainer.Say("Rest.");
        _.Timer.Wait(_.RNG.Between(5, 15));
        n = 0;
    }
}
