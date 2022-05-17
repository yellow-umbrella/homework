using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Поставщик шин")]
        public int TyreSupplierId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(1900, 2022, ErrorMessage = "Значення має бути в межах між 1900 та 2022")]
        [Display(Name = "Рік проведення")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Гоночний директор")]
        public string RaceDirector { get; set; } = null!;
        [Display(Name = "Правила")]
        public string? Rules { get; set; }

        [Display(Name = "Поставщик шин")]
        public virtual TyreSupplier TyreSupplier { get; set; } = null!;
        public virtual ICollection<DriverActivity> DriverActivities { get; set; }
        public virtual ICollection<Race> Races { get; set; }
    }
}
