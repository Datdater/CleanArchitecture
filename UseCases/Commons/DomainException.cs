namespace UseCases.Commons;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
