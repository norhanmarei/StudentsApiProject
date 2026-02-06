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


        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            var list = Student.GetPassedStudents();
            if (list.Count == 0) return NotFound("No Passed Students Found.");
            return Ok(list);
        }

        [HttpGet("Average", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<float> GetAverageGrade()
        {
            float avg = Student.GetAverageGrade();
            var list = Student.GetAllStudents();
            if (list.Count == 0) return NotFound("No Students Found.");
            return Ok(avg);
        }
    
        [HttpGet("{Id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudentById(int Id)
        {
            if (Id < 1) return BadRequest($"Bad Request: Id [{Id}] Is Invalid");
            var student = Student.GetStudentById(Id);
            if (student == null) return NotFound($"Student With Id [{Id}] Not Found.");
            var sDTO = student.studentDto;
            return Ok(sDTO);
        }
    }
}