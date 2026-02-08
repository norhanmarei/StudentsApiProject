using System.Data.Common;
using Npgsql.Replication;
using StudentDataAccessLayer;
namespace StudentApiBusinessLayer;

public class Student
{
    public enum enMode { AddNew = 0, Update = 1 };
    public enMode Mode = enMode.AddNew;
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Grade { get; set; }
    public StudentDTO studentDto { get { return (new StudentDTO(this.Id, this.Name, this.Age, this.Grade)); } }
    public Student(StudentDTO sDTO, enMode mode = enMode.AddNew)
    {
        this.Id = sDTO.Id;
        this.Name = sDTO.Name;
        this.Age = sDTO.Age;
        this.Grade = sDTO.Grade;
        Mode = mode;
    }


    private bool _addNewStudent()
    {
        this.Id = StudentData.AddNewStudent(studentDto);
        return (this.Id != -1);
    }
    private bool _updateStudent()
    {
        return StudentData.UpdateStudent(studentDto) > 0;
    }


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
    static public Student? GetStudentById(int Id)
    {
        StudentDTO studentDTO = StudentData.GetStudentById(Id);
        if (studentDTO == null) return null;
        return new Student(studentDTO, enMode.Update);
    }
    public bool Save()
    {
        switch (Mode)
        {
            case enMode.AddNew:
                {
                    if (_addNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                }
            case enMode.Update:
                {
                    return _updateStudent();
                }
        }
        return false;
    }
    public bool Delete(int Id)
    {
        return StudentData.Delete(Id) > 0;
    }
}