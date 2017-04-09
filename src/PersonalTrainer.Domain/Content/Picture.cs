using System.IO;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public class Picture
    {
        public Picture(string filename, string fullpath)
        {
            Name = Path.GetFileNameWithoutExtension(filename);
            Filename = filename;
            Fullpath = fullpath;
        }

        public string Name { get; }
        public string Filename { get; }
        public string Fullpath { get; }
    }
}