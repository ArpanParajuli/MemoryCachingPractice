using System.Collections.Generic;
namespace MemoryCachingPractice.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentDb();

        Task<Student> GetAllStudentCache();
    }
}
