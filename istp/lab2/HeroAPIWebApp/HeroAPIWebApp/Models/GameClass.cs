using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class GameClass
    {
        public GameClass()
        {
            Heroes = new List<Hero>();
            ClassSkills = new List<ClassSkill>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<ClassSkill> ClassSkills { get; set; }
    }
}
