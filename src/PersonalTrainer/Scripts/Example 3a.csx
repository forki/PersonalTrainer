var _ = Require<TrainingSession>();

// Since these scripts are C# we can write functions.  So lets make our stroking request 
// into a function like so:
void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

_.Content.Load("Pictures");

// Indeed we can do (almost) anything here we can do in C# only in a script with a text editor.
// So lets make a loop and have the player do an increasing number of strokes at an increasing 
// pace each time around the loop.  Still only once picture but we will get more very soon.

int bpm = 60;
int beats = 10;

// As well as getting the picture by name, we can also get them via C#'s query langauge LINQ.
// For example this gets us the first picture inthe collection we just loaded.
var picture = _.Content.Pictures.First();

for (int i = 0; i < 3; i++)
{
    _.Viewer.Display(picture);

    StrokePlease(beats, bpm);

    // We can clear the screen like this:
    _.Viewer.Clear();

    // And give the player a short 3 second rest like this:
    _.Timer.Wait(3);

    // Increase the speed and number of strokes for the next view.
    bpm += 20;
    beats += 10;
}

// So this example will require the player to do 10 beats at 60bpm, then 20 beats at 80bpm and
// finally 30 beats at 100bpm.

// Hopefully you can also see that it would be very easy to build up a library of useful functions
// like our simple example here. And so easily make more and more complicated training sessions.

// Our next example will look at how PERSONAL TRAINER can give instructions.
