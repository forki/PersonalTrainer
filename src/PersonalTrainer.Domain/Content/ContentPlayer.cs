using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Utilities;

namespace Figroll.PersonalTrainer.Domain.Content
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ContentPlayer : IContentViewer
    {
        public static readonly Picture BlankPicture = new Picture(string.Empty, string.Empty);

        private IEnumerable<Picture> _pictures = Enumerable.Empty<Picture>();
        private readonly ManualResetEvent _slideshowStopped = new ManualResetEvent(false);

        public void Load(IGallery gallery)
        {
            Load(gallery.Pictures);
        }

        public void Load(IEnumerable<Picture> pictures)
        {
            _pictures = pictures;
        }

        public void Display(Picture picture)
        {
            OnPictureChanged(new PictureEventArgs(picture));
        }

        public void Display(Picture picture, int thenPause)
        {
            Display(picture);
            Thread.Sleep(thenPause.ToMilliseconds());
        }

        public void Display(int pauseThen, Picture picture)
        {
            Thread.Sleep(pauseThen.ToMilliseconds());
            Display(picture);
        }

        public void Clear()
        {
            OnPictureChanged(new PictureEventArgs(BlankPicture));
        }

        public void Clear(int thenPause)
        {
            Clear();
            Thread.Sleep(thenPause * 1000);
        }

        public void PlaySlideshow(int displaySeconds)
        {
            PlaySlideshow(_pictures, () => displaySeconds);
        }

        public void PlaySlideshow(IGallery gallery, int displaySeconds)
        {
            PlaySlideshow(gallery.Pictures, () => displaySeconds);
        }

        public void PlaySlideshow(IEnumerable<Picture> pictures, int displaySeconds)
        {
            PlaySlideshow(pictures, () => displaySeconds);
        }

        public void PlaySlideshow(Func<int> calculateDisplaySeconds)
        {
            PlaySlideshow(_pictures, calculateDisplaySeconds());
        }

        public void PlaySlideshow(IGallery gallery, Func<int> calculateDisplaySeconds)
        {
            PlaySlideshow(gallery.Pictures, calculateDisplaySeconds());
        }

        public void PlaySlideshow(IEnumerable<Picture> pictures, Func<int> calculateDisplaySeconds)
        {
            OnSlideshowStarted();

            pictures.ForEach(p =>
            {
                OnPictureChanged(new PictureEventArgs(p));
                Thread.Sleep(calculateDisplaySeconds().ToMilliseconds());
            });

            OnSlideshowComplete();
        }

        public void WaitUntilComplete()
        {
            _slideshowStopped.WaitOne();
        }

        public event EventHandler SlideshowStarted;
        protected virtual void OnSlideshowStarted()
        {
            _slideshowStopped.Reset();
            SlideshowStarted?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<PictureEventArgs> PictureChanged;
        protected virtual void OnPictureChanged(PictureEventArgs e)
        {
            PictureChanged?.Invoke(this, e);
        }

        public event EventHandler SlideshowComplete;
        protected virtual void OnSlideshowComplete()
        {
            _slideshowStopped.Set();
            SlideshowComplete?.Invoke(this, EventArgs.Empty);
        }

    }
}