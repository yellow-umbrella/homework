using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class Circuite
    {
        public Circuite()
        {
            Races = new HashSet<Race>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Race> Races { get; set; }
    }
}
