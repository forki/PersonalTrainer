using System;
using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IContentViewer
    {
        void Load(IGallery gallery);
        void Load(IEnumerable<Picture> pictures);

        void Display(Picture picture);
        void Display(Picture picture, int thenPause);
        void Display(int pauseThen, Picture picture);

        void Clear();
        void Clear(int thenPause);

        void PlaySlideshow(int displaySeconds);
        void PlaySlideshow(IGallery gallery, int displaySeconds);
        void PlaySlideshow(IEnumerable<Picture> pictures, int displaySeconds);
        void PlaySlideshow(Func<int> calculateDisplaySeconds);
        void PlaySlideshow(IGallery gallery, Func<int> calculateDisplaySeconds);
        void PlaySlideshow(IEnumerable<Picture> pictures, Func<int> calculateDisplaySeconds);

        void WaitUntilComplete();

        event EventHandler SlideshowStarted;
        event EventHandler<PictureEventArgs> PictureChanged;
        event EventHandler SlideshowComplete;
    }
}