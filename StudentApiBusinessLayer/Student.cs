using StudentDataAccessLayer;
namespace StudentApiBusinessLayer;

public class Student
{
    static public List<StudentDTO> GetAllStudents()
    {
        return StudentData.GetAllStudents();
    }
    static public List<StudentDTO> GetPassedStudents()
    {
        return StudentData.GetPassedStudents();
    }
}