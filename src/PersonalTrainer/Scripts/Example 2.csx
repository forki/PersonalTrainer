var _ = Require<TrainingSession>();

// Our other option for playing the metronome is continuous play like this.
_.Metronome.BPM = 60;
_.Metronome.PlayUntilStopped();

// The metronome will now continue to play at 60 bpm until told to stop or the
// script finishes.

// In this case we will use the Timer to wait 10 seconds.  After which the script 
// will complete and the Metronome will stop.
_.Timer.Wait(10);

// On to Example 3!
