using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class Skill
    {

        public Skill()
        {
            ClassSkills = new List<ClassSkill>();
            RaceSkills = new List<RaceSkill>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ClassSkill> ClassSkills { get; set; }
        public virtual ICollection<RaceSkill> RaceSkills { get; set; }
    }
}
