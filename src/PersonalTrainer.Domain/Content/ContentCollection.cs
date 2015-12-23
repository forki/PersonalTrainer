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
        private readonly Dictionary<string, List<Picture>> _content = new Dictionary<string, List<Picture>>();
        private IEnumerable<Picture> _allPictures;

        public void Load(string mediaDirectory)
        {
            Load(mediaDirectory, new []{ ".jpg", ".png"});
            _allPictures = _content.Values.SelectMany(p => p);
        }

        public void Load(string mediaDirectory, string[] extensions)
        {
            _logger.Trace("Loading media dir={0}; ext={1}", mediaDirectory, extensions);

            _mediaBaseDirectory = Directory.GetParent(Path.GetFullPath(mediaDirectory)).FullName;

            LoadMedia(mediaDirectory, extensions);
            RemoveEmptyGalleries();

            _allPictures = _content.Values.SelectMany(p => p);
        }


        private void LoadMedia(string mediaDirectory, string[] extensions)
        {
            try
            {
                var files = Directory.GetFiles(mediaDirectory);
                var galleryName = Path.GetFileNameWithoutExtension(mediaDirectory);

                Debug.Assert(galleryName != null, "galleryName != null");
                _content.Add(galleryName, new List<Picture>());

                foreach (var file in files)
                {
                    var ext = Path.GetExtension(file);
                    if (ext == null || !extensions.Contains(ext.ToLower())) continue;

                    var name = Path.GetFileNameWithoutExtension(file);
                    var filename = Path.Combine(_mediaBaseDirectory, file);

                    _content[galleryName].Add(new Picture(name, filename));
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

        private void RemoveEmptyGalleries()
        {
            var emptyGalleries = _content.Where(x => x.Value.Count == 0).ToList();
            emptyGalleries.ForEach(g => _content.Remove(g.Key));
        }

        IEnumerable<Picture> IContentCollection.Pictures => _allPictures;
        IEnumerable<string> IContentCollection.Galleries => _content.Keys;

        public Picture GetPicture(string name)
        {
            return _allPictures.SingleOrDefault(p => p.Name == name) ?? ContentPlayer.BlankPicture;
        }

        public Picture GetPicture(string gallery, string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Picture> Gallery(string name)
        {
            return _content[name];
        }
    }
}