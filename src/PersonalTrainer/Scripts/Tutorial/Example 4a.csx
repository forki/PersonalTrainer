// Have your trainer speak our loud using Test To Speech.

void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

var _ = Require<TrainingSession>();

_.Content.Load("Pictures");
_.Viewer.Display(_.Content.Pictures.First(), 1);

// This is exactly the same as the previous example except we are 
// going to have the trainer speak using text to speech (TTS).

// First notice that if we do nothing then the trainer "speaks" using
// subtitles as in the previous example.

// However, if we select a TTS voice first then PERSONAL TRAINER will
// attempt to use that voice.  The simplest way to do that is to 
// tell the trainer to use the default voice.
_.Trainer.UseDefaultVoice();
_.Trainer.Say("Ten strokes please.");
StrokePlease(10, 120);

// Using the default voice will select a random, adult female voice
// and will try to use non-Microsoft voices (which tend to be paid for
// and therefore better).
// We can also ask for a specific voice like so:
_.Trainer.UseVoice("Zira");
_.Trainer.Say("Do ten more for Zira");
StrokePlease(10, 120);

// Note that the name of the voice does not need to be exact, so "Amy"
// will find a voice called "Acme Professional Voice Amy".

_.Viewer.Clear();
_.Trainer.UseVoice("Hazel");
_.Trainer.Say("Well done from Hazel", 1);
