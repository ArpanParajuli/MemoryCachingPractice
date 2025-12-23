using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemoryCachingPractice
{
    public class DataSeeder
    {
        private readonly StudentDbContext _context;
        private readonly ILogger<DataSeeder> _logger;
        private readonly List<string> _firstNames = new()
        {
            "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles",
            "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua",
            "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen",
            "Nancy", "Lisa", "Betty", "Helen", "Sandra", "Donna", "Carol", "Ruth", "Sharon", "Michelle"
        };

        private readonly List<string> _lastNames = new()
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson"
        };

        private readonly List<string> _domains = new()
        {
            "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "example.com", "university.edu", "student.org"
        };

        public DataSeeder(StudentDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedStudentsAsync(int count = 1000)
        {
            _logger.LogInformation("Seeding {Count} student records...", count);

            // Check if students already exist
            if (await _context.Students.AnyAsync())
            {
                _logger.LogInformation("Students already exist in the database. Skipping seeding.");
                return;
            }

            var random = new Random();
            var students = new List<Student>();

            for (int i = 1; i <= count; i++)
            {
                var firstName = _firstNames[random.Next(_firstNames.Count)];
                var lastName = _lastNames[random.Next(_lastNames.Count)];
                var domain = _domains[random.Next(_domains.Count)];

                // Create email with some variations
                var emailVariations = new[]
                {
                    $"{firstName.ToLower()}.{lastName.ToLower()}@{domain}",
                    $"{firstName.ToLower()}{lastName.ToLower()}@{domain}",
                    $"{firstName[0].ToString().ToLower()}{lastName.ToLower()}@{domain}",
                    $"{firstName.ToLower()}.{lastName[0].ToString().ToLower()}@{domain}",
                    $"{firstName.ToLower()}{random.Next(100)}@{domain}"
                };

                var email = emailVariations[random.Next(emailVariations.Length)];

                // Generate a random created date within the last 2 years
                var createdDate = DateTime.Now.AddDays(-random.Next(1, 730));

                // Generate a random last updated date between created date and now
                var lastUpdatedDate = createdDate.AddDays(random.Next(0, (DateTime.Now - createdDate).Days));

                // 10% chance of being deleted
                var isDeleted = random.NextDouble() < 0.1;
                var deletedAt = isDeleted ? lastUpdatedDate.AddDays(random.Next(1, 30)) : DateTime.MinValue;

                students.Add(new Student
                {
                    Pid = Guid.NewGuid(),
                    Name = $"{firstName} {lastName}",
                    Email = email,
                    Created = createdDate,
                    LastUpdated = lastUpdatedDate,
                    DeletedAt = deletedAt,
                    IsDeleted = isDeleted
                });

                // Batch insert every 100 records to avoid memory issues
                if (i % 100 == 0)
                {
                    await _context.Students.AddRangeAsync(students);
                    await _context.SaveChangesAsync();
                    students.Clear();
                    _logger.LogInformation("Seeded {Count} student records...", i);
                }
            }

            // Insert any remaining records
            if (students.Count > 0)
            {
                await _context.Students.AddRangeAsync(students);
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation("Successfully seeded {Count} student records.", count);
        }
    }
}