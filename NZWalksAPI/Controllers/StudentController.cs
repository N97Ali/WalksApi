using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    //https://Localhost:number/api/Student
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //get: https://Localhost:number/api/Student
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            string[] studentName = new string[] { "Ali", "Naushad0", "M" };
            return Ok(studentName);
        }
    }
}
 