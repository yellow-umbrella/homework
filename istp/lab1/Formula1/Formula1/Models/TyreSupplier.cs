using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class TyreSupplier
    {
        public TyreSupplier()
        {
            Seasons = new HashSet<Season>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Season> Seasons { get; set; }
    }
}
