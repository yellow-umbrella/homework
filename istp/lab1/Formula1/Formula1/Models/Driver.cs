using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Країна")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(1900, 2022, ErrorMessage = "Значення має бути в межах між 1900 та 2022")]
        [Display(Name = "Рік початку кар'єри")]
        public int CareerStartYear { get; set; }
        [Display(Name = "Країна")]
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<DriverActivity> DriverActivities { get; set; }
        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
