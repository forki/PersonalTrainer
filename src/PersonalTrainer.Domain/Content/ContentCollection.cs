using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Figroll.PersonalTrainer.Domain.API;
using NLog;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public class ContentCollection : IContentCollection
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        private string _mediaBaseDirectory;
        private readonly Dictionary<string, Gallery> _content = new Dictionary<string, Gallery>();
        private IEnumerable<Picture> _allPictures;

        public void Load(string mediaDirectory)
        {
            Load(mediaDirectory, new[] {".jpg", ".png"});
        }

        public void Load(string mediaDirectory, string[] extensions)
        {
            _logger.Trace("Loading media dir={0}; ext={1}", mediaDirectory, extensions);

            _mediaBaseDirectory = Directory.GetParent(Path.GetFullPath(mediaDirectory)).FullName;

            _content.Clear();
            LoadMedia(mediaDirectory, extensions);

            _allPictures = _content.Values.SelectMany(p => p.Pictures);
        }


        private void LoadMedia(string mediaDirectory, string[] extensions)
        {
            try
            {
                var files = Directory.GetFiles(mediaDirectory);
                var galleryName = Path.GetFileNameWithoutExtension(mediaDirectory);

                Debug.Assert(galleryName != null, "galleryName != null");

                var pictures = (from file in files
                    let ext = Path.GetExtension(file)
                    where ext != null && extensions.Contains(ext.ToLower())
                    let name = Path.GetFileName(file)
                    let filename = Path.Combine(_mediaBaseDirectory, file)
                    select new Picture(name, filename)).ToList();

                if (pictures.Count > 0)
                {
                    _content.Add(galleryName, new Gallery(galleryName, pictures));
                }

                foreach (var subdirectory in Directory.GetDirectories(mediaDirectory))
                {
                    LoadMedia(subdirectory, extensions);
                }
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Exception thrown reading " + mediaDirectory);
            }
        }

        IEnumerable<Picture> IContentCollection.Pictures => _allPictures;
        public IEnumerable<IGallery> Galleries => _content.Values;
        public IEnumerable<string> GalleryNames => _content.Keys;

        public Picture GetPicture(string name)
        {
            return _allPictures.SingleOrDefault(p => p.Name == name) ?? ContentPlayer.BlankPicture;
        }

        public Picture GetPicture(string gallery, string name)
        {
            throw new NotImplementedException();
        }

        public IGallery Gallery(string name)
        {
            return _content[name];
        }
    }
}