namespace Infrastructures.SQLServer.DataContext;

public class UserEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}