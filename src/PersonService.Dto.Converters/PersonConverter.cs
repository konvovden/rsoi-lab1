using DtoPerson = PersonService.Dto.Models.Person;
using CorePerson = PersonService.Core.Models.Person;

namespace PersonService.Dto.Converters;

public static class PersonConverter
{
    public static DtoPerson Convert(CorePerson corePerson)
    {
        return new DtoPerson(corePerson.Id,
            corePerson.Name,
            corePerson.Age,
            corePerson.Address,
            corePerson.Work);
    }
}