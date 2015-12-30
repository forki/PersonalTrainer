// This comment will form the description of this
// script.  We can have multiple lines.

var _ = Require<TrainingSession>();

    public void DoRest(int seconds)
{
    _.Timer.Wait(seconds);
    }

    public void DeepHard()
{
    _.Trainer.Say("Deep. Hard. Strokes.");
    _.Metronome.Play(10, 60);
    _.Metronome.WaitUntilPlayStops();
    }

    public void NextLevel()
{
    _.Trainer.Say("Cum for those cocks.");
    _.Metronome.Play(10, 120);
    _.Metronome.WaitUntilPlayStops();
    }

    _.Trainer.UseVoice("Amy");
    _.Content.Load("xmas");

    _.Trainer.Say("We are going to make you cum", 1);

    while (true)
{
    var loops = _.RNG.Between(5, 10);
    _.Timer.Wait(loops);

    while (loops-- > 0)
{
    DeepHard();
    DoRest(_.RNG.Between(3, 5));
    }

    DoRest(20);

    loops = _.RNG.Between(5, 10);
    _.Timer.Wait(loops);

    while (loops-- > 0)
{
    NextLevel();
    DoRest(_.RNG.Between(3, 5));
    }

    DoRest(20);

    _.Trainer.Say("Burst mode");
    _.Metronome.Play(20, 180);
    _.Metronome.WaitUntilPlayStops();
    DoRest(_.RNG.Between(3, 5));

    _.Trainer.Say("Burst mode");
    _.Metronome.Play(20, 180);
    _.Metronome.WaitUntilPlayStops();
    DoRest(_.RNG.Between(3, 5));

    _.Trainer.Say("Burst mode");
    _.Metronome.Play(20, 180);
    _.Metronome.WaitUntilPlayStops();
    DoRest(_.RNG.Between(3, 5));

    _.Trainer.Say("Cum for me");
    _.Metronome.Play(40, 180);
    _.Metronome.WaitUntilPlayStops();
    DoRest(10);

    _.Trainer.Say("Cum for me");
    _.Metronome.Play(40, 180);
    _.Metronome.WaitUntilPlayStops();
    DoRest(10);
    }
}