using Microsoft.EntityFrameworkCore;
using K_O_Project.Models.Entities;

namespace K_O_Project.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Entities.Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Login> Logins { get; set; }


    }
}
