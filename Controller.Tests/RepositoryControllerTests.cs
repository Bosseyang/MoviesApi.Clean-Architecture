using AutoMapper;
using Controller.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Moq;
using MovieApi.TestDemosOnly;
using Movies.Core.DomainContracts;
using Movies.Data;

namespace Controller.Tests;

internal class RepositoryControllerTests
{
    private readonly RepositoryController sut;
    private Mock<IMovieRepository> mockRepo;
    public RepositoryControllerTests()
    {
        var mockRepo = new Mock<IMovieRepository>();
        sut = new RepositoryController(mockRepo.Object, MapperFactory.Create());

    }

    //[fact]
    //public async Task GetMovie_ShouldReturnAllMovies()
    //{
    //    //Arrange
    //    const int expectedCount = 2;
    //    var expectedMovies = GetMovies(expectedCount);
    //    mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedCount);

    //    //Act

    //    //Assert
    //}

    //private List<Movie> GetMovies(int numberOfMovies)
    //{

    //}
}
