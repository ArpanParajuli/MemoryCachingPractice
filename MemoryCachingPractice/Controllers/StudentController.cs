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

        private int counter1 = 0;
        private int counter2 = 0;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }



        [HttpGet("student-nocache")]
        public async Task<IActionResult> GetAllStudents()
        {
            counter1++;
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            // db calls

            stopwatch.Stop();

            _logger.LogInformation($"GetAllStudents Method(); took {stopwatch.ElapsedMilliseconds} Total Requests! : {counter1}");
            return Ok("Student");
        }

        [HttpGet("student-cache")]
        public async Task<IActionResult> GetAllStudentsAsyncCache()
        {
            counter2++;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _logger.LogInformation($"GetAllStudentsAsyncCache Method(); took {stopwatch.ElapsedMilliseconds} Total Requests! : {counter2}");
            return Ok("Student");
        }
    }
}
