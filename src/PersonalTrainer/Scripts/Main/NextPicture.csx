var _ = Require<TrainingSession>();

_.Content.Load(@"D:\Milovana\OT London");
_.Trainer.UseVoice("Amy");

var shuffledGalleries = _.Content.Galleries.Shuffle();

int n = 0;
foreach (var picture in shuffledGalleries.SelectMany(gallery => gallery.Pictures))
{
    //_.Viewer.Display(picture);
    //    _.Trainer.Say("Stroke.");
    if (_.RNG.IsPercentageChance(10))
    {
        _.Trainer.Say("Test him.");
        _.Metronome.Play(_.RNG.Between(30, 60), 180);
    }
    else if (_.RNG.IsPercentageChance(10))
    {
        _.Trainer.Say("Deep. Hard. Strokes.");
        _.Metronome.Play(_.RNG.Between(20, 40), 60);
    }
    else if (_.RNG.IsPercentageChance(10))
    {
        _.Trainer.Say("Relentless.");
        _.Metronome.Play(_.RNG.Between(20, 40), 120);
    }
    else
        _.Metronome.Play(_.RNG.Between(5, 20), 120);

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