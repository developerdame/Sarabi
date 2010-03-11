using SharpArch.Core.DomainModel;

namespace Sarabi.Core
{
    public class Image : Entity
    {
        public Image()
        {
        }

        public Image(string type, string url)
        {
            Url = url;
            Type = type.Substring(type.LastIndexOf('/'));
        }

        public virtual string Url { get; set; }
        public virtual string Type { get; set; }
    }
}