namespace UseCase.ViewModels.StudentViewModels;

public class StudentViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}