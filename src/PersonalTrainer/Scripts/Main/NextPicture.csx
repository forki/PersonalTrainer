var _ = Require<TrainingSession>();

_.Content.Load(@"pack");
_.Trainer.UseVoice("Amy");

var shuffledGalleries = _.Content.Galleries.Shuffle();

foreach (var gallery in _.Content.Galleries)
{
    _.Trainer.Say(gallery.Name, 2);

    int n = 0;
    foreach (var picture in gallery.Pictures)
    {
        _.Viewer.Display(picture);

        _.Metronome.Play(10, 80);
        _.Metronome.WaitUntilPlayStops();

        _.Viewer.Clear();

        n++;
        if (_.RNG.Between(1, 100) > 75)
        {
            _.Trainer.Say("Stop.");
            _.Timer.Wait(_.RNG.Between(2, 5));
        }
        else if (n >= 9)
        {
            _.Trainer.Say("Rest.");
            _.Timer.Wait(_.RNG.Between(10, 30));
            n = 0;
        }
    }

    _.Trainer.Say("Well done.");
    _.Timer.Wait(_.RNG.Between(10, 30));

}