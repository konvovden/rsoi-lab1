using PersonService.Core.Models;

namespace PersonService.Core.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetAllPersonsAsync();
    Task<Person> GetPersonAsync(int id);

    Task<Person> CreatePersonAsync(string name,
        int? age,
        string? address,
        string? work);

    Task<Person> UpdatePersonAsync(int id,
        string name,
        int? age,
        string? address,
        string? work);

    Task<Person> DeletePersonAsync(int id);
}