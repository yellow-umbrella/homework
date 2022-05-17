using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class Team
    {
        public Team()
        {
            DriverActivities = new HashSet<DriverActivity>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(1900, 2022, ErrorMessage = "Значення має бути в межах між 1900 та 2022")]
        [Display(Name = "Рік заснування")]
        public int FoundationYear { get; set; }

        public virtual ICollection<DriverActivity> DriverActivities { get; set; }
    }
}
