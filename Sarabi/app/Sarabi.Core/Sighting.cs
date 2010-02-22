using System.Drawing;
using SharpArch.Core.DomainModel;

namespace Sarabi.Core
{
    public class Sighting : Entity
    {
        public virtual Celebrity Celebrity { get; set; }
        public virtual Coordinates Coordinates { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual User SeenBy { get; set; }

    }
}