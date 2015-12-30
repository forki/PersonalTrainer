// Using the C# programming language to improve our scripts.
var _ = Require<TrainingSession>();

// Since these scripts are C# we can write functions.  So let's make our stroking request 
// into a function like so:
void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

_.Content.Load("Pictures");

// Indeed we can do (almost) anything here we can do in C# only as a script with a text editor
// instead of a development environment and compiler.
// So lets make a loop and have the player do an increasing number of strokes at an increasing 
// pace each time around the loop.  
// Still only one picture but we will get to multiple pictures very soon.

// Let's start at 10 strokes at 60bpm.
int bpm = 60;
int count = 10;

// As well as getting the picture by name, we can also get them via C#'s query langauge LINQ.
// For example this gets us the first picture in the collection we just loaded.
var picture = _.Content.Pictures.First();

for (int i = 0; i < 3; i++)
{
    // Note when we display a picture we can pause briefly before continuing with the
    // the script.  This can help with the flow so the picture doesn't appear and 
    // the beats start at exactly the same time.
    _.Viewer.Display(picture, 1);

    // Which is identical to these two statements
    //      _.Viewer.Display(picture);
    //      _.Timer.Wait(1);

    // The picture has been displayed, we have paused for one second so the player can take 
    // it in, let's now call our function and get them to do count strokes at a speed  of bpm
    StrokePlease(count, bpm);

    // We can clear the screen by calling Viewer.Clear()
    // And give the player a short 3 second rest like this:
    //    _.Viewer.Clear();
    //    _.Timer.Wait(3);
    
    // Or again we could do both in one line:
    _.Viewer.Clear(3);

    // Increase the speed and number of strokes for the next view.
    bpm += 20;
    count += 10;
}

// So this example will require the player to do 10 beats at 60bpm, then 20 beats at 80bpm and
// finally 30 beats at 100bpm.

// Hopefully you can also see that it would be very easy to build up a library of useful functions
// like those in our simple example here. And so easily make more and more complicated training sessions.

// Our next example will look at how PERSONAL TRAINER can give instructions.
