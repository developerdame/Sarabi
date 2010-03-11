using System.Collections.Generic;
using SharpArch.Core.DomainModel;

namespace Sarabi.Core
{
    public class Celebrity : Entity
    {
        public Celebrity()
        {
            Images = new List<Image>();
            Sightings = new List<Sighting>();
        }

        public virtual string Name { get; set; }
        public virtual double Rank { get; set; }
        public virtual string WikipediaUrl { get; set; }
		public virtual string Type { get; set; }
        public virtual string Abstract { get; set; }
        public virtual string DbpediaResourceId { get; set; }
        public virtual string HomepageUrl { get; set; }
        public virtual List<Sighting> Sightings { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}