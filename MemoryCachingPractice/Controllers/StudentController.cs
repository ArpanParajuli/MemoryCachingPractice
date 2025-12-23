using MemoryCachingPractice.Migrations;
using MemoryCachingPractice.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace MemoryCachingPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        private readonly IStudentRepository _repo;




        public StudentController(ILogger<StudentController> logger , IStudentRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }



        [HttpGet("student-db")]
        public async Task<IActionResult> GetAllStudentsAsyncDb()
        {
            var stopwatch = Stopwatch.StartNew();

            var students = await _repo.GetAllStudentDbAsync();
            // db calls

            stopwatch.Stop();

            _logger.LogInformation($"GetAllStudents Method(); took {stopwatch.ElapsedMilliseconds}");
            return Ok(students);
        }


        [HttpGet("student-cache")]
        public async Task<IActionResult> GetAllStudentsAsyncCache()
        {

            var stopwatch = Stopwatch.StartNew();

            var students = await _repo.GetAllStudentCache(); // async method

            stopwatch.Stop();
            _logger.LogInformation($"GetAllStudentsAsyncCache Method(); took {stopwatch.ElapsedMilliseconds} {stopwatch.}");
            return Ok(students); 
        }
    }
}
