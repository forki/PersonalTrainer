using System;
using System.Collections.Generic;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public interface IContentViewer
    {
        void Load(IEnumerable<Picture> pictures);

        void PlaySlideshow();
        void PlaySlideshow(IEnumerable<Picture> pictures);

        IObservable<Picture> WhenPictureChanged { get; }
        void WaitUntilComplete();

        void Display(Picture picture);
        //void Display(Picture picture, int thenPause);

        void Clear();
        //void Clear(int thenPause);
    }
}