using Sarabi.Core;

namespace Sarabi.ApplicationServices
{
    public class CelebFinderContext
    {
        public string Name { get; set; }
        public string WikipediaUrl { get; set; }
        public Celebrity Celebrity { get; set; }
    }
}