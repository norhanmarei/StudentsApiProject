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
    static public float GetAverageGrade()
    {
        return Student.GetAverageGrade();
    }
    static public StudentDTO? GetStudentById(int Id)
    {
        return StudentData.GetStudentById(Id);
    }

}