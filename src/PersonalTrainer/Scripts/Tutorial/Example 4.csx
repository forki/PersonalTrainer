﻿// How to have your trainer speak.

void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

var _ = Require<TrainingSession>();

// Some people need to be told what to do.
// So lets do 10 strokes to blue socks again but have our trainer tell the player what
// is expected of him / her / them :-)

_.Content.Load("Pictures");
_.Viewer.Display(_.Content.Pictures.First(), 1);

// This will display a subtitle on top of the blue socks picture.
// Note that the subtitle will be displayed for a brief time to give the 
// player time to read.  The display time is based on the on the number of words 
// being displayed.
_.Trainer.Say("Ten strokes please.");

StrokePlease(10, 120);

_.Viewer.Clear();
_.Trainer.Say("Well done.", 1);

// Next up in Example 5 we will look at random numbers.
