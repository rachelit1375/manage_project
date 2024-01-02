
namespace BO;
public class BlDoesNotExistException : Exception //Dose not exist
{
    public BlDoesNotExistException(string? message) : base(message) { }//Sends to the parent class
    public BlDoesNotExistException(string message, Exception innerException)//If the department receives 2 exceptions
                : base(message, innerException) { }
}

public class BlAlreadyExistsException : Exception //Already exist
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlPropertyException : Exception //Error in details
{
    public BlPropertyException(string? message) : base(message) { }
}
public class BlDeletionImpossible : Exception //Error in details
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}
