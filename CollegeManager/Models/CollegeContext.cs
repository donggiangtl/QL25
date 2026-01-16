using System.Data.Entity;

namespace CollegeManager.Models
{
    public class CollegeContext : DbContext
    {
        public CollegeContext() : base(""DefaultConnection"") { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
