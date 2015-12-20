using System.Collections.Generic;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public interface IContentCollection
    {
        void Load(string mediaDirectory);
        void Load(string mediaDirectory, string[] extensions);

        IEnumerable<Picture> Pictures { get; }
        IEnumerable<Picture> Gallery(string name);
        IEnumerable<string> Galleries { get; }

        Picture GetPicture(string name);
        Picture GetPicture(string gallery, string name);
    }
}