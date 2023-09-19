using Microsoft.EntityFrameworkCore;
using PersonService.Core.Exceptions;
using PersonService.Core.Models;
using PersonService.Core.Repositories;
using PersonService.Database.Context;
using PersonService.Database.Repositories.Converters;

using DbPerson = PersonService.Database.Models.Person;

namespace PersonService.Database.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonServiceContext _dbContext;

    public PersonRepository(PersonServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        var persons = await _dbContext.Persons
            .AsNoTracking()
            .ToListAsync();

        return persons.ConvertAll(PersonConverter.Convert);
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        var person = await _dbContext.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (person is null)
            throw new PersonNotFoundException(id);

        return PersonConverter.Convert(person);
    }

    public async Task<Person> CreatePersonAsync(string name, 
        int? age,
        string? address,
        string? work)
    {
        var person = new DbPerson(name,
            age,
            address,
            work);

        await _dbContext.Persons.AddAsync(person);
        await _dbContext.SaveChangesAsync();

        return PersonConverter.Convert(person);
    }

    public async Task<Person> UpdatePersonAsync(int id, 
        string name,
        int? age,
        string? address,
        string? work)
    {
        var person = await _dbContext.Persons.FindAsync(id);

        if (person is null)
            throw new PersonNotFoundException(id);

        person.Name = name;
        person.Age = age;
        person.Address = address;
        person.Work = work;

        await _dbContext.SaveChangesAsync();

        return PersonConverter.Convert(person);
    }

    public async Task<Person> DeletePersonAsync(int id)
    {
        var person = await _dbContext.Persons.FindAsync(id);

        if (person is null)
            throw new PersonNotFoundException(id);

        _dbContext.Persons.Remove(person);

        await _dbContext.SaveChangesAsync();

        return PersonConverter.Convert(person);
    }
}