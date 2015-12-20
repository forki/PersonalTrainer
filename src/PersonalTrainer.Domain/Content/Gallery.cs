using System.Collections.Generic;
using System.Reflection;
using NLog;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public class Gallery : IGallery
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public string Name { get; private set; }
        public IEnumerable<Picture> Pictures { get; private set; }

        public Gallery(string name, IEnumerable<Picture> pictures)
        {
            Name = name;
            Pictures = pictures;
        }
    }
}