using Microsoft.EntityFrameworkCore;

namespace MemoryCachingPractice.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;
        public StudentRepository(StudentDbContext context)
        { 
           _context = context;
        }

        public async Task <IEnumerable<Student>> GetAllStudentDbAsync()
        {
            var students = await _context.Students.ToListAsync();
            return students;
        }  

        //public async Task<Student> GetAllStudentCache()
        //{
            
        //}
    }
}
