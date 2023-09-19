using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using OptionalTypes;

namespace PersonService.Dto.Requests;

[DataContract]
public class PatchPersonRequest
{
    [Required]
    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }

    [DataMember(Name = "age", EmitDefaultValue = false)]
    public Optional<int?> Age { get; set; }

    [DataMember(Name = "address", EmitDefaultValue = false)]
    public Optional<string?> Address { get; set; }

    [DataMember(Name = "work", EmitDefaultValue = false)]
    public Optional<string?> Work { get; set; }

    public PatchPersonRequest()
    {
        Name = string.Empty;
    }
}
