using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class Companion
    {
        public Companion()
        {
            Heroes = new List<Hero>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 100)]
        public int Rating { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Hero> Heroes { get; set; }
    }
}
