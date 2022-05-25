using System.ComponentModel.DataAnnotations;

namespace HeroAPIWebApp.Models
{
    public class Hero
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public int RaceId { get; set; }
        public int ClassId { get; set; }
        public int CompanionId { get; set; }

        public virtual Race Race { get; set; }
        public virtual Class Class { get; set; }
        public virtual Companion Companion { get; set; }
    }
}
