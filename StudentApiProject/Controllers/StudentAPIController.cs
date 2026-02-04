using Microsoft.AspNetCore.Mvc;
using StudentApi.DataSimulation;
using StudentApi.Models;
using System.Collections.Generic;
namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(StudentDataSimulation.StudentsList);
        }
    }
}