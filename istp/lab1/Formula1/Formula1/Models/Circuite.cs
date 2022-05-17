using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class Circuite
    {
        public Circuite()
        {
            Races = new HashSet<Race>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Країна")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;
        [Display(Name = "Країна")]
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Race> Races { get; set; }
    }
}
