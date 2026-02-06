using StudentDataAccessLayer;
namespace StudentApiBusinessLayer;

public class Student
{
    static public List<StudentDTO> GetAllStudents()
    {
        return StudentData.GetAllStudents();
    }
}
