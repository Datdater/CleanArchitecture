namespace UseCase.Commons;

public class AppConfiguration
{
    public string DatabaseConnection { get; set; }
    public string RedisConnection { get; set; }
    public string JWTSecretKey { get; set; }

}