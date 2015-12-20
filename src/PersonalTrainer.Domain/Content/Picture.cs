namespace Figroll.PersonalTrainer.Domain.Content
{
    public class Picture
    {
        public string Name { get; }
        public string Filename { get; }

        public Picture(string name, string filename)
        {
            Name = name;
            Filename = filename;
        }
    }
}