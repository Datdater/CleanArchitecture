namespace API.Models;

public class StudentModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }
}