var _ = Require<TrainingSession>();

// Let's load the content in the "Collection" folder.  This has three sub folders
// (Mikela, Petra V and Rachel H) which are loaded as Galleries named after the 
// folders we found when loading the content.
_.Content.Load("Collection");

// We can get at these galleries using the name and then play a slideshow. And if 
// use set the metronome in continuous play we have a very basic "show and stroke" session.
_.Metronome.BPM = 120;
_.Metronome.Start();

var gallery = _.Content.Gallery("Petra V");
_.Viewer.PlaySlideshow(gallery, 5);

// The slideshow will play at 5 seconds per picture while the metronome keeps a 
// constant 120 bpm.  So that the script won't end we can wait here until the slideshow
// is done just like we did with the metronome.
_.Viewer.WaitUntilComplete();

_.Metronome.Stop();
_.Trainer.Say("Well done.");




