using System.Data;
using Npgsql;
namespace StudentDataAccessLayer;

public class StudentData
{
    static string _connectionString = "Server=localhost; Database=studentdb; User Id=postgres; Password=Hi!bluhDuh99";
    public static List<StudentDTO> GetAllStudents()
    {
        List<StudentDTO> StudentList = new List<StudentDTO>();
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("SELECT * FROM GetAllStudents()", conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StudentList.Add(new StudentDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetInt32(reader.GetOrdinal("Age")),
                            reader.GetInt32(reader.GetOrdinal("Grade"))
                        ));
                    }
                }
            }
        }
        return StudentList;
    }
    public static List<StudentDTO> GetPassedStudents()
    {
        var list = new List<StudentDTO>();
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("SELECT * FROM GetPassedStudents()", conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StudentDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader.GetInt32(reader.GetOrdinal("Age")),
                            reader.GetInt32(reader.GetOrdinal("Grade"))

                        ));
                    }
                }
            }
        }
        return list;
    }
    public static float GetAverageGrade()
    {
        float averageGrade = 0;
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("SELECT GetAverageGrade();", conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value) averageGrade = Convert.ToSingle(result);

            }
        }
        return averageGrade;
    }
    public static StudentDTO? GetStudentById(int Id)
    {
        int ID = 0, Age = 0, Grade = 0;
        string Name = "";
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand($"SELECT * FROM GetStudentById({Id})", conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("Id"));
                        Name = reader.GetString(reader.GetOrdinal("Name"));
                        Age = reader.GetInt32(reader.GetOrdinal("Age"));
                        Grade = reader.GetInt32(reader.GetOrdinal("Grade"));
                        return new StudentDTO(ID, Name, Age, Grade);
                    }
                }
            }
        }
        return null;
    }
    public static int AddNewStudent(StudentDTO sDTO)
    {
        int Id = -1;
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("SELECT AddStudent(@Name, @Age, @Grade)", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Name", sDTO.Name);
                cmd.Parameters.AddWithValue("@Age", sDTO.Age);
                cmd.Parameters.AddWithValue("@Grade", sDTO.Grade);
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value) Id = Convert.ToInt32(result);
            }
        }
        return Id;
    }
    public static int UpdateStudent(StudentDTO sDTO)
    {
        int affectedRows = 0;
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            using (var cmd = new NpgsqlCommand("SELECT UpdateStudent(@Id, @Name, @Age, @Grade)", conn))
            {
                cmd.Parameters.AddWithValue("@Id", sDTO.Id);
                cmd.Parameters.AddWithValue("@Name", sDTO.Name);
                cmd.Parameters.AddWithValue("@Age", sDTO.Age);
                cmd.Parameters.AddWithValue("@Grade", sDTO.Grade);
                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value) affectedRows = Convert.ToInt32(result);
            }
        }
        return affectedRows;
    }
    public static int Delete(int Id)
    {
        int affectedRows = 0;
        using(var conn = new NpgsqlConnection(_connectionString))
        {
            using(var cmd = new NpgsqlCommand("SELECT DeleteStudent(@StudentID)", conn))
            {
                cmd.Parameters.AddWithValue("@StudentID", Id);
                conn.Open();
                var res = cmd.ExecuteScalar();
                if (res != null && res != DBNull.Value) affectedRows = Convert.ToInt32(res);
            }
        }
        return affectedRows;
    }
}
public class StudentDTO
{
    public StudentDTO(int Id, string Name, int Age, int Grade)
    {
        this.Id = Id;
        this.Name = Name;
        this.Age = Age;
        this.Grade = Grade;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int Grade{ get; set; }
}
