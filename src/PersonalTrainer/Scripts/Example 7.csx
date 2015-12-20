var _ = Require<TrainingSession>();

void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

int bpm = 60;
int beats = 10;

while (true)
{
    StrokePlease(beats, bpm);

    // We can clear the screen like this:
    _.Viewer.Clear();

    // And give the player a short 3 second rest like this:
    _.Timer.Wait(3);

    // Increase the speed and number of strokes for the next view.
    bpm += 20;
    beats += 10;
}

