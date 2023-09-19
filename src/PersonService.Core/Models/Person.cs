namespace PersonService.Core.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string? Address { get; set; }
    public string? Work { get; set; }

    public Person(int id,
        string name, 
        int? age, 
        string? address,
        string? work)
    {
        Id = id;
        Name = name;
        Age = age;
        Address = address;
        Work = work;
    }
}