using System.Collections.Generic;

namespace Figroll.PersonalTrainer.Domain.Content
{
    public interface IGallery
    {
        string Name { get; }
        IEnumerable<Picture> Pictures { get; }
    }
}