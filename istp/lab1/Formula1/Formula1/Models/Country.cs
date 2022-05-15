using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class Country
    {
        public Country()
        {
            Circuites = new HashSet<Circuite>();
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Circuite> Circuites { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
