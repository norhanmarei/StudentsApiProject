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
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if (StudentDataSimulation.StudentsList.Count == 0) return NotFound("No Students Found.");
            return Ok(StudentDataSimulation.StudentsList);
        }
        
        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            if (StudentDataSimulation.StudentsList.Count == 0) return NotFound("No Students Found.");
            var PassedStudents = StudentDataSimulation.StudentsList.Where(student => student.Grade >= 50);
            return Ok(PassedStudents);
        }
        [HttpGet("Average", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        {
            //StudentDataSimulation.StudentsList.Clear();
            if (StudentDataSimulation.StudentsList.Count == 0) return NotFound("No Students Found.");
            var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);
            return Ok(averageGrade);
        }
        [HttpGet("{Id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentById(int Id)
        {
            if (Id < 1) return BadRequest($"Bad Request: Not Accepted Id [{Id}]");
            var student = StudentDataSimulation.StudentsList.Find(s => s.Id == Id);
            if (student == null) return NotFound($"Not Found: Student With Id [{Id}] Not Found.");
            return Ok(student);
        }
    }
}