using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1
{
    public partial class Country
    {
        public Country()
        {
            Circuites = new HashSet<Circuite>();
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Circuite> Circuites { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
