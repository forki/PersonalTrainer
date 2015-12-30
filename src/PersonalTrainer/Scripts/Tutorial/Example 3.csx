// Loading content and displaying a picture.
var _ = Require<TrainingSession>();

// Beats are all very well but what about something to look at?
// As well as a Metronome, your PERSONAL TRAINER can also display Content.

// We first need to tell PERSONAL TRAINER where the content we want is.
// This will load the Pictures folder from the directory in which PERSONAL TRAINER
// was installed.
_.Content.Load("Pictures");

// We can also load from a completely different directory so this would work exactly as you 
// might expect.  (We need the @ to stop the \s confusing C#)
//_.Content.Load(@"C:\My Collection\Pictures");

// The Pictures folder has one picture in it called "blue socks.jpg" which we can display like this.
var picture = _.Content.GetPicture("blue socks.jpg");
_.Viewer.Display(picture);

// Or in one line like this:
// _.Viewer.Display("blue socks.jpg");

// The picture is now on the screen and our script continues.
// Let's play 10 beats at 120 again.
_.Metronome.BPM = 120;
_.Metronome.Play(10);

// And wait until the beats are done.
_.Metronome.WaitUntilPlayStops();

// In example 3a we are going to do the same as this example but add some programming refinements.
