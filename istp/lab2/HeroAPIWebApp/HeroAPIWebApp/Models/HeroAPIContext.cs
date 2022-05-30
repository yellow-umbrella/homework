using Microsoft.EntityFrameworkCore;

namespace HeroAPIWebApp.Models
{
    public class HeroAPIContext : DbContext
    {
        public virtual DbSet<Hero> Heroes { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<GameClass> GameClasses { get; set; }
        public virtual DbSet<RaceSkill> RaceSkills { get; set; }
        public virtual DbSet<ClassSkill> ClassSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Companion> Companions { get; set; }

        public HeroAPIContext(DbContextOptions<HeroAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
