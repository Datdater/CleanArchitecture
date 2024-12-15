using Entities;
using UseCase;
using UseCases.UnitOfWork;

namespace UseCases.Implementation;

public class ManageStudent(IUnitOfWork unitOfWork): IManageStudent
{
    public async Task AddStudentAsync(Student student)
    {
        var studentRepository = unitOfWork.Students;
        await studentRepository.InsertAsync(student);
        await unitOfWork.SaveAsync();
    }

    public Task UpdateStudentAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStudentAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Student>> GetAllStudentsAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<Student> GetStudentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}