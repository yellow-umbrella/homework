using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class ClassSkill
    {
        public int Id { get; set; }
        [Required]
        public int GameClassId { get; set; }
        [Required]
        public int SkillId { get; set; }

        public virtual GameClass GameClass { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
