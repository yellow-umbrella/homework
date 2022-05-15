using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class Season
    {
        public Season()
        {
            DriverActivities = new HashSet<DriverActivity>();
            Races = new HashSet<Race>();
        }

        public int Id { get; set; }
        public int TyreSupplierId { get; set; }
        public int Year { get; set; }
        public string RaceDirector { get; set; } = null!;
        public string? Rules { get; set; }

        public virtual TyreSupplier TyreSupplier { get; set; } = null!;
        public virtual ICollection<DriverActivity> DriverActivities { get; set; }
        public virtual ICollection<Race> Races { get; set; }
    }
}
