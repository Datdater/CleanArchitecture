using Entities;
using UseCase.Commons;
using UseCase.ViewModels.StudentViewModels;

namespace UseCase;

public interface IManageStudent
{
    public Task<StudentViewModel> AddStudentAsync(CreateStudentViewModel student);
    public Task UpdateStudentAsync(Student student);
    public Task DeleteStudentAsync(Student student);
    public Task<Pagination<Student>> GetAllStudentsAsync(int pageIndex, int pageSize);
    public Task<StudentViewModel> GetStudentByIdAsync(int id);
}