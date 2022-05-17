using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class DriverActivity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Команда")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Сезон")]
        public int SeasonId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Гонщик")]
        public int DriverId { get; set; }

        [Display(Name = "Гонщик")]
        public virtual Driver Driver { get; set; } = null!;
        [Display(Name = "Сезон")]
        public virtual Season Season { get; set; } = null!;
        [Display(Name = "Команда")]
        public virtual Team Team { get; set; } = null!;
    }
}
