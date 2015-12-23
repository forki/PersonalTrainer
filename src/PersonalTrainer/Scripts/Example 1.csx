// You must always include this in your script.
var _ = Require<TrainingSession>();

// PERSONAL TRAINER is now initialised and can do a bunch of useful things.

// For example, it has a metronome and we can set the BPM and play 
// any number of beats. This will play 10 beats at 120bpm.
_.Metronome.BPM = 120;
_.Metronome.Play(10);

// Note that the metronome plays in the backgound and our script continues
// to the next statement.  If there were no more statements then PERSONAL TRAINER 
// would will go back to the script selection screen and the metronome will continue 
// to play until the beats stop.

// This is probably not what we want so we can wait until the beats have played.
_.Metronome.WaitUntilPlayStops();

// The script is now complete and will exit and we will return to the script
// selection screen.
