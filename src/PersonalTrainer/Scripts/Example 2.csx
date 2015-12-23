var _ = Require<TrainingSession>();

// We can also just start the Metronome playing.
_.Metronome.BPM = 60;
_.Metronome.Start();

// The metronome will now continue to play at 60 bpm until told to stop or the
// script finishes.

// In this case we will use the Timer to wait 10 seconds and then stop the Metronome.
_.Timer.Wait(10);

_.Metronome.Stop();

// On to Example 3!
