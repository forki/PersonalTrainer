using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public class ContentPlayer : IContentViewer
    {
        public static readonly Picture BlankPicture = new Picture(string.Empty, string.Empty);

        private readonly ManualResetEvent _slideshowStopped = new ManualResetEvent(false);

        private readonly Subject<Picture> _pictureChanged = new Subject<Picture>();

        private IEnumerable<Picture> _pictures = Enumerable.Empty<Picture>();
        private IObservable<Unit> _currentPlayback;

        public void Load(IEnumerable<Picture> pictures)
        {
            _pictures = pictures;
        }

        public void PlaySlideshow()
        {
            PlaySlideshow(_pictures);
        }

        public void PlaySlideshow(IEnumerable<Picture> pictures)
        {
            _pictures = pictures;

            _slideshowStopped.Reset();

            _currentPlayback = Observable.Start(() =>
            {
                _pictures.ForEach(p =>
                {
                    _pictureChanged.OnNext(p);
                    Thread.Sleep(1000);
                });

                _pictureChanged.OnCompleted();
                _slideshowStopped.Set();
            });
        }

        public void Display(Picture picture)
        {
            _pictureChanged.OnNext(picture);
        }

        public void Clear()
        {
            _pictureChanged.OnNext(BlankPicture);
        }

        public void Clear(int thenPause)
        {
            throw new NotImplementedException();
        }

        public IObservable<Picture> WhenPictureChanged => _pictureChanged.AsObservable();

        public void WaitUntilComplete()
        {
            // if IsPlaying to prevent deadlock?

            _slideshowStopped.WaitOne();

            // cannot async/await as scriptcs does not support it
            //await _currentPlayback;
        }
    }
}