using System.Collections.Generic;
namespace MemoryCachingPractice.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentDbAsync();

        Task<IEnumerable<Student>> GetAllStudentCache();
    }
}
