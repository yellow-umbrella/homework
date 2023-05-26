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


public class Student {

    public Student() 
    {
        Results = new List<Result>();
    }

    public int Id { get; set; }
    [MaxLength(25)]
    [MinLength(3)]
    public string StudentName { get; set; }
    [MaxLength(25)]
    [MinLength(3)]
    public string StudentPhone { get; set; }
    [MaxLength(25)]
    [MinLength(3)]
    public string StudentInfo { get; set; }
    [MaxLength(25)]
    [MinLength(3)]
    public string StudentEmail { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}

public class Result
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    [Range(1,12)]
    public int Rating { get; set; }

    public virtual Student Student { get; set; }
    public virtual Subject Subject { get; set; }

}

public class Subject
{
    public Subject()
    {
        Results = new List<Result>();
    }

    public int Id { get; set; }
    [MaxLength(25)]
    [MinLength(3)]
    public string SubjectName { get; set; }
    [MaxLength(25)]
    [MinLength(3)]
    public string SubjectIndo { get; set; }

    public virtual ICollection<Result> Results { get; set; }
}

public class TestContext : DbContext
{
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Result> Results { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}

[HttpGet]
public async Task<ActionResult<IEnumerable<Student>>> Get()
{
    var res = _context.Students.OrderBy(s => _context.Results.Where(r => r.StudentId == c.Id).Average(s => s.Rating));
    if (_context.ClassSkills == null)
    {
        return NotFound();
    }
    return await res.ToListAsync().Take(5);
}