public void DoSet(int strokes, int speed)
{
	_.Trainer.Say("Ready.");
	_.Sequencer.Play(strokes, speed);
	_.Sequencer.WaitUntilPlayStops();
	_.Trainer.Say("Stop.");
}

public void DoRest(int seconds)
{
    _.Timer.Wait(seconds);
}

public void DoLongerRest(int seconds)
{
    _.Trainer.SayAsync("Rest now.");
    _.Timer.Wait(seconds);
}

void DoExercise()
{
	var repeats = _.RNG.Between(6, 12);

	for (int n = 0; n < repeats; n++)
	{
		var speed = speeds.ElementAt(_.RNG.Between(0, speeds.Count()));
		var time = times.ElementAt(_.RNG.Between(0, times.Count()));

		DoSet(time, speed);
		DoRest(_.RNG.Between(8, 16));
	}
}

public void DoLevelOne()
{
    _.Sequencer.SetPattern("1234");

    speeds.Clear();
    speeds.Add(60);
    speeds.Add(80);
    speeds.Add(100);

    DoExercise();

    _.Trainer.Say("Well done.");

    DoLongerRest(_.RNG.Between(16, 24));
}

public void DoLevelOnePointFive()
{
    _.Sequencer.SetPattern("1234");

    speeds.Clear();
    speeds.Add(100);
    speeds.Add(120);
    speeds.Add(140);

    DoExercise();

    _.Trainer.Say("Well done.");
}

public void DoLevelTwo()
{
	_.Sequencer.SetPattern("123+4");

	speeds.Clear();
	speeds.Add(120);
	speeds.Add(140);
	speeds.Add(160);

	DoExercise();

	_.Trainer.Say("Well done.");
}

public void DoLevelThree()
{
	_.Sequencer.SetPattern("1+23+4");
	_.Trainer.Say("Level three.");

	speeds.Clear();
	speeds.Add(90);
	speeds.Add(120);
	speeds.Add(140);

	DoExercise();

	_.Trainer.Say("Well done.");
}

var speeds = new List<int>();

var times = new List<int>();
times.Add(8);
times.Add(10);
times.Add(14);
times.Add(16);

var rests = new List<int>();
rests.Add(10);
rests.Add(20);
rests.Add(30);

var _ = Require<TrainingSession>();

var pictureInfo = new PictureInfo(null, null);
_.Trainer.UseVoice("Amy");


while (true)
{
    DoLevelOne();
    DoLevelOne();
    DoLevelOne();
    DoLevelTwo();
}

//_.Metronome.BPM = 60;
//_.Metronome.Play(2);
//_.Timer.Wait(3);
//_.Metronome.Stop();


