using System;
using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.Content;

namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IContentViewer
    {
        void Load(IEnumerable<Picture> pictures);
        void Load(IGallery gallery);

        void PlaySlideshow(int displaySeconds);
        void PlaySlideshow(IGallery gallery, int displaySeconds);
        void PlaySlideshow(IEnumerable<Picture> pictures, int displaySeconds);

        IObservable<Picture> WhenPictureChanged { get; }
        void WaitUntilComplete();

        void Display(string picture);
        void Display(string gallery, string picture);

        void Display(Picture picture);
        void Display(Picture picture, int thenPause);
        void Display(int pauseThen, Picture picture);

        // There should be a DisplayNext()?

        void Clear();
        void Clear(int thenPause);
    }
}