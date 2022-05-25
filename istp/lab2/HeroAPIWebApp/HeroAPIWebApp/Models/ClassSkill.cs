using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class ClassSkill
    {
        public int ClassId { get; set; }
        public int SkillId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
