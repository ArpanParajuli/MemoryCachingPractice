using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCachingPractice.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        private readonly IMemoryCache _memoryCache;

        private const string StudentsCacheKey = "students:sabai";
        public StudentRepository(StudentDbContext context , IMemoryCache memoryCache)
        { 
           _context = context;
           _memoryCache = memoryCache;
        }

        public async Task <IEnumerable<Student>> GetAllStudentDbAsync()
        {
            var students = await _context.Students
                .AsNoTracking()
                .ToListAsync();
            return students;
        }

        public async Task<IEnumerable<Student>> GetAllStudentCache()
        {
            if (_memoryCache.TryGetValue(StudentsCacheKey , out List<Student> students))
            {
                return students;
            }

            students = await _context.Students.AsNoTracking().ToListAsync();

            _memoryCache.Set(StudentsCacheKey, students, TimeSpan.FromMinutes(60));

            return students;
        }
    }
}
