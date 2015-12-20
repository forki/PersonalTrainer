namespace Figroll.PersonalTrainer.Domain.Content
{
    public class PictureRequestedEventArgs : System.EventArgs
    {
        public string ImageLocation { get; private set; }

        public PictureRequestedEventArgs(string imageLocation)
        {
            ImageLocation = imageLocation;
        }
    }
}