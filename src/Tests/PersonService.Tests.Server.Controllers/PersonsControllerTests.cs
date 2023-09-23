using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonService.Core.Exceptions;
using PersonService.Core.Models;
using PersonService.Core.Repositories;
using PersonService.Server.Controllers;

using CorePerson = PersonService.Core.Models.Person;
using DtoPerson = PersonService.Dto.Models.Person;

namespace PersonService.Tests.Server.Controllers;

public class PersonsControllerTests
{
    [Fact]
    public async Task ListPersons_OkResult()
    {
        // Arrange
        var corePersons = new List<Person>()
        {
            new Person(1, "PersonName1", 55, "PersonAddress1", "PersonWork1"),
            new Person(2, "PersonName2", null, null, null)
        };
        
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(r => r.GetAllPersonsAsync()).ReturnsAsync(corePersons);

        var personController = new PersonsController(personRepositoryMock.Object);
        
        // Act
        var result = await personController.ListPersons();
        var objectResult = result as ObjectResult;

        // Assert
        Assert.NotNull(objectResult);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        Assert.IsAssignableFrom<IEnumerable<DtoPerson>>(objectResult.Value);

        var resultPersons = (IEnumerable<DtoPerson>) objectResult.Value!;
        Assert.Equal(corePersons.Count, resultPersons.Count());
        Assert.All(resultPersons, (person, i) => AssertPersonsEqual(corePersons[i], person));
    }

    [Fact]
    public async Task GetPerson_OkResult()
    {
        // Arrange
        var corePerson = new Person(1, "PersonName1", 55, "PersonAddress1", "PersonWork1");
        
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock
            .Setup(r => r.GetPersonAsync(It.IsAny<int>()))
            .ReturnsAsync(corePerson);

        var personController = new PersonsController(personRepositoryMock.Object);
        
        // Act
        var result = await personController.GetPerson(It.IsAny<int>());
        var objectResult = result as ObjectResult;

        // Assert
        Assert.NotNull(objectResult);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        Assert.IsAssignableFrom<DtoPerson>(objectResult.Value);

        var resultPerson = (DtoPerson) objectResult.Value!;
        AssertPersonsEqual(corePerson, resultPerson);
    }
    
    [Fact]
    public async Task GetPerson_NotFoundResult()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock
            .Setup(r => r.GetPersonAsync(It.IsAny<int>()))
            .ThrowsAsync(new PersonNotFoundException());

        var personController = new PersonsController(personRepositoryMock.Object);
        
        // Act
        var result = await personController.GetPerson(It.IsAny<int>());
        var statusCodeResult = result as StatusCodeResult;

        // Assert
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public async Task DeletePerson_OkResult()
    {
        // Arrange
        var corePerson = new Person(1, "PersonName1", 55, "PersonAddress1", "PersonWork1");
        
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock
            .Setup(r => r.DeletePersonAsync(It.IsAny<int>()))
            .ReturnsAsync(corePerson);

        var personController = new PersonsController(personRepositoryMock.Object);
        
        // Act
        var result = await personController.DeletePerson(It.IsAny<int>());
        var statusCodeResult = result as StatusCodeResult;

        // Assert
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status204NoContent, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public async Task DeletePerson_NotFoundResult()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock
            .Setup(r => r.DeletePersonAsync(It.IsAny<int>()))
            .ThrowsAsync(new PersonNotFoundException());

        var personController = new PersonsController(personRepositoryMock.Object);
        
        // Act
        var result = await personController.DeletePerson(It.IsAny<int>());
        var statusCodeResult = result as StatusCodeResult;
        
        // Assert
        Assert.NotNull(statusCodeResult);
        Assert.Equal(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
    }

    private void AssertPersonsEqual(CorePerson corePerson, DtoPerson dtoPerson)
    {
        Assert.Equal(corePerson.Id, dtoPerson.Id);
        Assert.Equal(corePerson.Name, dtoPerson.Name);
        Assert.Equal(corePerson.Age, dtoPerson.Age);
        Assert.Equal(corePerson.Address, dtoPerson.Address);
        Assert.Equal(corePerson.Work, dtoPerson.Work);
    }
}