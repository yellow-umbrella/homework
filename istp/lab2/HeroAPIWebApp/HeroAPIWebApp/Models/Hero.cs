using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class Hero
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 100)]
        public int Level { get; set; }
        [Required]
        public int RaceId { get; set; }
        [Required]
        public int GameClassId { get; set; }
        [Required]
        public int CompanionId { get; set; }

        public virtual Race Race { get; set; }
        public virtual GameClass GameClass { get; set; }
        public virtual Companion Companion { get; set; }
    }
}
