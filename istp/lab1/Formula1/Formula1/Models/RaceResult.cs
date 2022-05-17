using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class RaceResult
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Гонка")]
        public int RaceId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Гонщик")]
        public int DriverId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(1, 30, ErrorMessage = "Значення має бути в межах між 1 та 30")]
        [Display(Name = "Зайняте місце")]
        public int Place { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(0, 100, ErrorMessage = "Значення має бути в межах між 1 та 100")]
        [Display(Name = "Отримані бали")]
        public int Points { get; set; }

        [Display(Name = "Гонщик")]
        public virtual Driver Driver { get; set; } = null!;
        [Display(Name = "Гонка")]
        public virtual Race Race { get; set; } = null!;
    }
}
