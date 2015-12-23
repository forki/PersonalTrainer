using System.Collections.Generic;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public class Gallery : IGallery
    {
        public string Name { get; private set; }
        public IEnumerable<Picture> Pictures { get; private set; }

        public Gallery(string name, IEnumerable<Picture> pictures)
        {
            Name = name;
            Pictures = pictures;
        }
    }
}