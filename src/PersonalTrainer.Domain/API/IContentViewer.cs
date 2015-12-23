using System;
using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IContentViewer
    {
        void Load(IEnumerable<Picture> pictures);

        void PlaySlideshow(int displaySeconds);
        void PlaySlideshow(IEnumerable<Picture> pictures, int displaySeconds);

        IObservable<Picture> WhenPictureChanged { get; }
        void WaitUntilComplete();

        void Display(Picture picture);
        void Display(Picture picture, int thenPause);
        void Display(int pauseThen, Picture picture);

        // Should be a DisplayNext()

        void Clear();
        void Clear(int thenPause);
    }
}