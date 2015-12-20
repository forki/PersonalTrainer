void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

var _ = Require<TrainingSession>();

_.Content.Load("Collection");

var gallery = _.Content.Gallery("Petra V");
_.ContentPlayer.Play(gallery);

_.ContentPlayer.WaitUntilComplete();

// Lets load the content in the "collection" folder.  This has three sub folders
// (Mikela, Petra V and Rachel H) which are loaded as Galleries.
//_.Content.Load("Collection");
//
//// Lets make the player do 10 strokes to each picture in the Petra V gallery
//
//// First select the Petra V gallery.
//_.Content.SelectGallery("Petra V");
//
//// And then display the next picture until we run out with a simple while loop.
//// And for each picture do 10 strokes.
//while (_.Content.DisplayNext().IsPicture)
//{
//    StrokePlease(10, 120);
//    _.Timer.Wait(1);
//}
//
_.Trainer.Say("Well done.");




