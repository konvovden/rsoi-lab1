using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PersonService.Core.Exceptions;
using PersonService.Core.Repositories;
using PersonService.Dto.Converters;
using PersonService.Dto.Models;
using PersonService.Dto.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace PersonService.Server.Controllers;

[ApiController]
[Route("/api/v1/persons")]
public class PersonsController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonsController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    /// <summary>
    /// Create new Person
    /// </summary>
    /// <param name="personRequest"></param>
    /// <response code="201">Created new Person</response>
    /// <response code="400">Invalid data</response>
    [HttpPost]
    [SwaggerOperation("CreatePerson")]
    public async Task<IActionResult> CreatePerson([FromBody]Person personRequest)
    {
        var person = await _personRepository.CreatePersonAsync(personRequest.Name,
            personRequest.Age,
            personRequest.Address,
            personRequest.Work);
        
        return Created($"/api/v1/persons/{person.Id}", PersonConverter.Convert(person));
    }

    /// <summary>
    /// Update Person by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="personRequest"></param>
    /// <response code="200">Person for ID was updated</response>
    /// <response code="400">Invalid data</response>
    /// <response code="404">Not found Person for ID</response>
    [HttpPatch("{id:int}")]
    [SwaggerOperation("EditPerson")]
    [SwaggerResponse(statusCode: 200, type: typeof(Person), description: "Person for ID was updated")]
    public async Task<IActionResult> EditPerson([FromRoute][Required]int id, [FromBody]PatchPersonRequest personRequest)
    {
        try
        {
            var person = await _personRepository.GetPersonAsync(id);
            
            var updatedPerson = await _personRepository.UpdatePersonAsync(id, 
                personRequest.Name,
                personRequest.Age.GetValueOrDefault(person.Age),
                personRequest.Address.GetValueOrDefault(person.Address),
                personRequest.Work.GetValueOrDefault(person.Work));

            return Ok(PersonConverter.Convert(updatedPerson));
        }
        catch (PersonNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Remove Person by ID
    /// </summary>
    /// <param name="id"></param>
    /// <response code="204">Person for ID was removed</response>
    [HttpDelete("{id:int}")]
    [SwaggerOperation("DeletePerson")]
    public async Task<IActionResult> DeletePerson([FromRoute][Required]int id)
    {
        try
        {
            await _personRepository.DeletePersonAsync(id);

            return NoContent();
        }
        catch (PersonNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get Person by ID
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Person for ID</response>
    /// <response code="404">Not found Person for ID</response>
    [HttpGet("{id:int}")]
    [SwaggerOperation("GetPerson")]
    [SwaggerResponse(statusCode: 200, type: typeof(Person), description: "Person for ID")]
    public async Task<IActionResult> GetPerson([FromRoute][Required]int id)
    {
        try
        {
            var person = await _personRepository.GetPersonAsync(id);

            return Ok(PersonConverter.Convert(person));
        }
        catch (PersonNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get all Persons
    /// </summary>
    /// <response code="200">All Persons</response>
    [HttpGet]
    [SwaggerOperation("ListPersons")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Person>), description: "All Persons")]
    public async Task<IActionResult> ListPersons()
    {
        var persons = await _personRepository.GetAllPersonsAsync();
        
        return Ok(persons.ConvertAll(PersonConverter.Convert));
    }
}