using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class RaceSkill
    {
        public int Id { get; set; }
        [Required]
        public int RaceId { get; set; }
        [Required]
        public int SkillId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
