
namespace DO;
[Serializable]

public class DalDoesNotExistException : Exception//Dose not exist
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
public class DalAlreadyExistsException : Exception//Already exist
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
public class DalDeletionImpossible : Exception//Cannot delete
{
    public DalDeletionImpossible(string? message) : base(message) { }
}
public class DalXMLFileLoadCreateException : Exception//
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}
