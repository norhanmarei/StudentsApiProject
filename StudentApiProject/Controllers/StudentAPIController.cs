using Microsoft.AspNetCore.Mvc;
using StudentApiBusinessLayer;
using StudentDataAccessLayer;
//using StudentApi.DataSimulation;
//using StudentApi.Models;
using System.Collections.Generic;
namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            var list = Student.GetAllStudents();
            if (list.Count == 0) return NotFound("No Students Found.");
            return Ok(list);
        }

    }
}