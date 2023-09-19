namespace PersonService.Core.Exceptions;

public class PersonNotFoundException : Exception
{
    public PersonNotFoundException()
    {

    }

    public PersonNotFoundException(string? message) : base(message)
    {

    }

    public PersonNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {

    }
    
    public PersonNotFoundException(int id): base($"Person with id {id} not found")
    {
        
    }
}