using System.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Sarabi.Core
{
    public class Celebrity : Entity
    {
        public virtual string Name { get; set; }
        public virtual double Rank { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual string WikipediaUrl { get; set; }
        public virtual List<Occupation> Occupations { get; set; }
        public virtual List<Sighting> Sightings { get; set; }
    }
}