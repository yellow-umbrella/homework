using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class Driver
    {
        public Driver()
        {
            DriverActivities = new HashSet<DriverActivity>();
            RaceResults = new HashSet<RaceResult>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; } = null!;
        public int CareerStartYear { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<DriverActivity> DriverActivities { get; set; }
        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
