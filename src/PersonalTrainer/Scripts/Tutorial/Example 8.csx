// How to randomise the display of pictures and galleries.
var _ = Require<TrainingSession>();

_.Content.Load("Collection");

_.Metronome.BPM = 120;
_.Metronome.Start();

// We can randomise the order of the galleries and the order of the slideshow
// using Shuffle(), like so:

var shuffledGalleries = _.Content.Galleries.Shuffle();

// shuffledGalleries is now our three loaded galleries in a random order.
// Note that _.Content.Galleries is still in the original order.

foreach (var gallery in shuffledGalleries)
{
    // We can also shuffle the pictures in the gallery and pass that 
    // to the slideshow viewer.  This is an easy way to get "random no
    // repeats" type behaviour.
    var shuffledPictures = gallery.Pictures.Shuffle();
    _.Viewer.PlaySlideshow(shuffledPictures, 1);

    _.Viewer.WaitUntilComplete();
}

_.Metronome.Stop();
_.Trainer.Say("Well done.");
