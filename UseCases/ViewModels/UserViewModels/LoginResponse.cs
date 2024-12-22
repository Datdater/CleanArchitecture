namespace UseCase.ViewModels.UserViewModels;

public class LoginResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}