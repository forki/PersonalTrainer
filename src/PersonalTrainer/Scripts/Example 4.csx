var _ = Require<TrainingSession>();

// Some people need to be told what to do.
// So lets do 10 strokes to blue socks again but have our trainer tell the player what
// is expected of him / her / them :-)

_.Content.Load("Pictures");

_.Content.Load("Pictures");
_.Content.Display("blue socks.jpg");

var picture = _.Content.Pictures.First();
_.ContentViewer.Display(picture);
_.Timer.Wait(3);


// This will display a subtitle on top of the blue socks picture.
_.Trainer.Say("Ten strokes please.");

_.Metronome.BPM = 120;
_.Metronome.Play(10);

// And wait until the beats are done.
_.Metronome.WaitUntilPlayStops();

_.Trainer.Say("Well done.");

// In example 5 we will add some random numbers and show how we can introduce delays and timers.

