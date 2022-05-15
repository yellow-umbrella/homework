using System;
using System.Collections.Generic;

namespace Formula1
{
    public partial class TyreSupplier
    {
        public TyreSupplier()
        {
            Seasons = new HashSet<Season>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Season> Seasons { get; set; }
    }
}
