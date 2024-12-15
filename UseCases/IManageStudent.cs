using Entities;

namespace UseCase;

public interface IManageStudent
{
    public Task AddStudentAsync(Student student);
    public Task UpdateStudentAsync(Student student);
    public Task DeleteStudentAsync(Student student);
    public Task<IEnumerable<Student>> GetAllStudentsAsync(Student student);
    public Task<Student> GetStudentByIdAsync(int id);
}