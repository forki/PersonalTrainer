namespace Figroll.PersonalTrainer.Domain.Content
{
    public class PictureInfo
    {
        public bool IsBlank => string.IsNullOrEmpty(Picture);
        public bool IsPicture => !IsBlank;

        public string Gallery { get; }
        public string Picture { get; }

        public PictureInfo(string gallery, string picture)
        {
            Gallery = gallery;
            Picture = picture;
        }
    }
}