using AutoMapper;
using Entities;
using UseCase;
using UseCase.Commons;
using UseCase.ViewModels.StudentViewModels;
using UseCases.UnitOfWork;

namespace UseCases.Implementation;

public class ManageStudent(IUnitOfWork unitOfWork, IMapper mapper): IManageStudent
{

    public async Task<StudentViewModel> AddStudentAsync(CreateStudentViewModel student)
    {
        var studentRepository = unitOfWork.StudentsRepository;
        await studentRepository.InsertAsync(mapper.Map<Student>(student));
        var isSuccess = await unitOfWork.SaveAsync() > 0;
        if (isSuccess)
        {
            return mapper.Map<StudentViewModel>(student);
        }

        return null;
    }

    public Task UpdateStudentAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStudentAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public async Task<Pagination<Student>> GetAllStudentsAsync(int pageIndex, int pageSize)
    {
        var students = await unitOfWork.StudentsRepository.ToPagination(pageIndex, pageSize);
     //   var students = await unitOfWork.StudentsRepository.ToPagination(pageIndex, pageSize);
        Console.WriteLine(students);
        return students;
    }

    Task<StudentViewModel> IManageStudent.GetStudentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

}