var _ = Require<TrainingSession>();

_.Content.Load("Collection");

_.Metronome.BPM = 120;
_.Metronome.Start();

// This example is very similar except we are going to play all three Galleries 
// in our collection one after the other.

foreach (var gallery in _.Content.Galleries)
{
    _.Viewer.PlaySlideshow(gallery, 1);

    // The slideshow will play at 5 seconds per picture while the metronome keeps a 
    // constant 120 bpm. Again becuase the slideshow is playing in the background we
    // need to wait until it's finished before moving on to the next gallery.
    _.Viewer.WaitUntilComplete();
    }

    _.Metronome.Stop();
    _.Trainer.Say("Well done.");
}