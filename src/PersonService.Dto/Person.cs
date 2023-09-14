using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PersonService.Dto;

[DataContract]
public class Person
{
    [Required]
    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }

    [DataMember(Name = "age", EmitDefaultValue = false)]
    public int Age { get; set; }

    [DataMember(Name = "address", EmitDefaultValue = false)]
    public string Address { get; set; }

    [DataMember(Name = "work", EmitDefaultValue = false)]
    public string Work { get; set; }

    public Person(string name, 
        int age,
        string address,
        string work)
    {
        Name = name;
        Age = age;
        Address = address;
        Work = work;
    }
}