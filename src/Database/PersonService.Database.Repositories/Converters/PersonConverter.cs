using DbPerson = PersonService.Database.Models.Person;
using CorePerson = PersonService.Core.Models.Person;

namespace PersonService.Database.Repositories.Converters;

public static class PersonConverter
{
    public static CorePerson Convert(DbPerson dbPerson)
    {
        return new CorePerson(dbPerson.Id,
            dbPerson.Name,
            dbPerson.Age,
            dbPerson.Address,
            dbPerson.Work);
    }
}