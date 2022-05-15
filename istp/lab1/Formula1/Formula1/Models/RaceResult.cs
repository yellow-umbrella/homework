using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class RaceResult
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public int DriverId { get; set; }
        public int Place { get; set; }
        public int Points { get; set; }

        public virtual Driver Driver { get; set; } = null!;
        public virtual Race Race { get; set; } = null!;
    }
}
