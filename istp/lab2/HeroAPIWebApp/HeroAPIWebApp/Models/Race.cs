using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class Race
    {
        public Race()
        {
            Heroes = new List<Hero>();
            RaceSkills = new List<RaceSkill>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<RaceSkill> RaceSkills { get; set; }
    }
}
