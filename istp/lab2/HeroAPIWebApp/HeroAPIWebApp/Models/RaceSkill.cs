using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class RaceSkill
    {
        public int RaceId { get; set; }
        public int SkillId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
