﻿void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

var _ = Require<TrainingSession>();

_.Content.Load("Pictures");
var picture = _.Content.GetPicture("blue socks");

_.Trainer.Say("Look at this picture without stroking untill I tell you to stop.");
_.Viewer.Display(picture);

// So that the time looking at the picture will be random we will generate a 
// random number between 5 and 10 and have the player wait that many seconds.
// We do this using the Random Number Generator like so:
var pauseSeconds = _.RNG.Between(5, 10);
_.Timer.Wait(pauseSeconds);

// The following should all make sense now. Including the use or pauses in
// the calls to Clear(), Display(), etc.
_.Viewer.Clear(1);
_.Trainer.Say("Now do ten strokes to the same picture please.", 1);
_.Viewer.Display(picture, 1);
StrokePlease(10, 120);
_.Viewer.Clear(1);
_.Trainer.Say("Well done.", 1);

// In example 6 we will look at Galleries and how we can display sequences of pictures.


