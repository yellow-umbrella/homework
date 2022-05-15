using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class DriverActivity
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public int DriverId { get; set; }

        public virtual Driver Driver { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
        public virtual Team Team { get; set; } = null!;
    }
}
