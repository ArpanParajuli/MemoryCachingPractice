using Microsoft.EntityFrameworkCore;


namespace MemoryCachingPractice
{
    public class StudentDbContext : DbContext
    {
        private readonly ILogger<StudentDbContext> _logger;
        public StudentDbContext(DbContextOptions options , ILogger<StudentDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<Student> Students { get; set; }

      
    }
}
