using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class Class
    {
        public Class()
        {
            Heroes = new List<Hero>();
            ClassSkills = new List<ClassSkill>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Hero> Heroes { get; set; }
        public virtual ICollection<ClassSkill> ClassSkills { get; set; }
    }
}
