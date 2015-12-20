void StrokePlease(int count, int bpm)
{
    _.Metronome.BPM = bpm;
    _.Metronome.Play(count);
    _.Metronome.WaitUntilPlayStops();
}

var _ = Require<TrainingSession>();

var info = new PictureInfo(null, null);
_.Trainer.UseVoice("Amy");
_.Content.Load("Endless");

int score = 0;
var lastGallery = string.Empty;
do
{
    int n = 0;

    while (info.IsPicture || info.Picture == null)
    {
        if (info.Picture == null)
        {
            info = _.Content.DisplayNext();
        }

        if (info.Gallery != lastGallery)
        {
            lastGallery = info.Gallery;
            score += 100;
        }

        score++;

        if (n == 0)
        {
            score += 10;
        }

        int tempo = 60 + (n * 20);

        StrokePlease(20, tempo);
        _.Timer.Wait(1);

        if (n++ > 10)
        {
            n = 0;
        }

        info = _.Content.DisplayNext();

        if (info.IsBlank)
        {
            _.Content.SelectNextGallery();
            _.Timer.Wait(4);
            info = new PictureInfo(null, null);
        }
    }
    _.Trainer.SayAsync("Stop.");
    _.Content.Clear(5);

} while (_.Content.SelectNextGallery());
