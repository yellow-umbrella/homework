using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class Race
    {
        public Race()
        {
            RaceResults = new HashSet<RaceResult>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Сезон")]
        public int SeasonId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Траса")]
        public int CircuiteId { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        //[Range(1900, 2022, ErrorMessage = "Значення має бути в межах між 1900 та 2022")]
        [Display(Name = "Дата проведення")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Range(1, 100, ErrorMessage = "Значення має бути в межах між 1 та 100")]
        [Display(Name = "Кількість кіл")]
        public int LapCount { get; set; }

        [Display(Name = "Траса")]
        public virtual Circuite Circuite { get; set; } = null!;
        [Display(Name = "Сезон")]
        public virtual Season Season { get; set; } = null!;
        public virtual ICollection<RaceResult> RaceResults { get; set; }
    }
}
