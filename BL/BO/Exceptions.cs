
namespace BO;
public class BlDoesNotExistException : Exception //Dose not exist
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlAlreadyExistsException : Exception //Already exist
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlNullPropertyException : Exception 
{
    public BlNullPropertyException(string? message) : base(message) { }
}

