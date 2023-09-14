using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PersonService.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace PersonService.Server.Controllers;

[ApiController]
[Route("/api/v1/persons")]
public class PersonsController : ControllerBase
{ 
    /// <summary>
    /// Create new Person
    /// </summary>
    /// <param name="personRequest"></param>
    /// <response code="201">Created new Person</response>
    /// <response code="400">Invalid data</response>
    [HttpPost]
    [SwaggerOperation("CreatePerson")]
    public virtual IActionResult CreatePerson([FromBody]Person personRequest)
    {
        throw new NotImplementedException();
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
    public virtual IActionResult EditPerson([FromRoute][Required]int id, [FromBody]Person personRequest)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Remove Person by ID
    /// </summary>
    /// <param name="id"></param>
    /// <response code="204">Person for ID was removed</response>
    [HttpDelete("{id:int}")]
    [SwaggerOperation("DeletePerson")]
    public virtual IActionResult DeletePerson([FromRoute][Required]int id)
    { 
        throw new NotImplementedException();
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
    public virtual IActionResult GetPerson([FromRoute][Required]int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get all Persons
    /// </summary>
    /// <response code="200">All Persons</response>
    [HttpGet]
    [SwaggerOperation("ListPersons")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Person>), description: "All Persons")]
    public virtual IActionResult ListPersons()
    {
        throw new NotImplementedException();
    }
}