using System.Data.Entity;

namespace StudentAdminPortal.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("StudentDbConnection")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<User> Users { get; set; }
    }
}