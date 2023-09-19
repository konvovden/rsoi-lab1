using PersonService.Dto;

namespace PersonService.Adapters.Interfaces;

public interface IPersonRepositoryAdapter
{
    Task<List<Person>> GetAllPersonsAsync();
    Task<Person> GetPersonAsync(int id);

    Task<Person> CreatePersonAsync(int id,
        string name,
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