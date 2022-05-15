using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class Race
    {
        public Race()
        {
            RaceResults = new HashSet<RaceResult>();
        }

        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int CircuiteId { get; set; }
        public DateTime Date { get; set; }
        public int LapCount { get; set; }

        public virtual Circuite Circuite { get; set; } = null!;
        public virtual Season Season { get; set; } = null!;
        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
