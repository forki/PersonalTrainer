// Since this is just C# we can make functions to save repetition.
// So we could just make a DoTenStrokes() function like this:
void DoTenStrokes()
{
    _.Metronome.BPM = 120;
    _.Metronome.Play(10);
    _.Metronome.WaitUntilPlayStops();
}

// Or better still make the funciton allow us to specify the bpm and 
// the number of strokes.
void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

var _ = Require<TrainingSession>();

_.Content.Load("Pictures");

_.Trainer.Say("Look at this picture without stroking untill I tell you to stop.");
_.Content.Display("blue socks.jpg");

// So that the time looking at the picture will be random we will generate a 
// random number between 5 and 10 and have the player wait that many seconds.
// We do this using the Random Number Generator like so:
var pauseSeconds = _.RNG.Between(5, 10);

// This will pause for pause seconds.
_.Timer.Wait(pauseSeconds);

// We can remove the picture and display a black screen like this:
_.Content.Clear();

// And give the player their next instruction.
_.Trainer.Say("Now do ten strokes to the same picture please.");

// We want to display the picture, wait one second and then play
// the metronome as the 1 second delay makes it "flow" better.
// We would do this like this:
//
//      _.Content.Display("blue socks.jpg", 1);
//      _.Timer.Wait(1);
//
// Or in one statement like this:
_.Content.Display("blue socks.jpg", 1);

// And call the function we wrote earlier
StrokePlease(10, 120);

// Most of the functions we can call have parameters to build in these little delays
// which keeps your scrips cleaner and more concise.
// So here we will clear the picture and wait one second and then congratulate the player.
_.Content.Clear(1);
_.Trainer.Say("Well done.");

// In example 6 we will look at Galleries.


