var _ = Require<Training_>();

// Lets play 10 beats at 120 again.
_.Metronome.BPM = 120;
_.Metronome.Play(10);

// But this time lets wait until the beats finish.
_.Metronome.WaitUntilPlayStops();

// The script will now pause until the 10 beats have 
// finished and then go back to the script selection screen.

// On to Example 3!
