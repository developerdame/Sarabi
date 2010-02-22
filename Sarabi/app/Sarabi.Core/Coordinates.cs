using SharpArch.Core.DomainModel;

namespace Sarabi.Core
{
    public class Coordinates : Entity
    {
        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }
        
    }
}