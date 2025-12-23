using System.Collections.Generic;
namespace MemoryCachingPractice.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentDbAsync();

        //Task<Student> GetAllStudentCache();
    }
}
