using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class Team
    {
        public Team()
        {
            DriverActivities = new HashSet<DriverActivity>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int FoundationYear { get; set; }

        public virtual ICollection<DriverActivity> DriverActivities { get; set; }
    }
}
