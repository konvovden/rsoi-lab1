namespace PersonService.Database.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string? Address { get; set; }
    public string? Work { get; set; }

    public Person(string name, 
        int? age, 
        string? address,
        string? work)
    {
        Name = name;
        Age = age;
        Address = address;
        Work = work;
    }
}