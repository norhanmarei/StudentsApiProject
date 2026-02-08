using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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


        [HttpPost(Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<StudentDTO> AddNewStudent(StudentDTO sDTO)
        {
            if (sDTO == null || sDTO.Age < 0 || sDTO.Grade < 0 || string.IsNullOrEmpty(sDTO.Name)) return BadRequest("Bad Request: Invalid Student Data.");
            var student = new Student(new StudentDTO(sDTO.Id, sDTO.Name, sDTO.Age, sDTO.Grade), Student.enMode.AddNew);
            if (student.Save())
            {
                sDTO.Id = student.Id;
                return CreatedAtRoute("GetStudentById", new { id = sDTO.Id }, sDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Internal Server Error: Error Occurred While Adding, Student Not Added" });
            }

        }


        [HttpPut("{Id}" ,Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdateStudent(int Id, StudentDTO sDTO)
        {
            if (sDTO == null || sDTO.Age < 0 || sDTO.Grade < 0 || string.IsNullOrEmpty(sDTO.Name) || Id < 1)
                return BadRequest("Bad Request: Invalid Student Info.");
            var student = Student.GetStudentById(Id);
            if (student == null) return NotFound($"Not Found: Student With [{Id}] Is Not Found.");
            student.Name = sDTO.Name;
            student.Age = sDTO.Age;
            student.Grade = sDTO.Grade;
            if (student.Save())
                return Ok(student.studentDto);
            else
                return StatusCode(500, new { message = "Internal Server Error: Error Occurred While Updating, Student Not Updated" });
        }
    }
}