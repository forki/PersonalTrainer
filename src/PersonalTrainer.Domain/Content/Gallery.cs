using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public class Gallery : IGallery
    {
        public Gallery(string name, IEnumerable<Picture> pictures)
        {
            Name = name;
            Pictures = pictures;
        }

        public string Name { get; }
        public IEnumerable<Picture> Pictures { get; }
    }
}