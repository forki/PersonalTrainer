// You must always include this in your script.
var _ = Require<Training_>();

// PERSONAL TRAINER is now initialised and can do a bunch of useful things.

// For example, it has a metronome and we can set the BPM and play 
// any number of beats.
// This will play 10 beats at 120bpm.
_.Metronome.BPM = 120;
_.Metronome.Play(10);

// Note that the metronome plays in the backgound and our script continues
// to the next statement.  And since there are no more statements in this
// script PERSONAL TRAINER will go back to the script selection screen and
// the metronome will continue to play until the beats stop.

// This is probably not what we want so time for Example 2.