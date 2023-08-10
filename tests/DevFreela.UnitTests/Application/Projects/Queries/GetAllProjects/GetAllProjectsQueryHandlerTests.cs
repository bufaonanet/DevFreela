using DevFreela.Application.Projects.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_ProjectViewModelList()
    {
        // Arrange
        var projects = new List<Project>
        {
            new Project("Nome Do Teste 1", "Descricao De Teste 1", 1, 2, 10000),
            new Project("Nome Do Teste 2", "Descricao De Teste 2", 1, 2, 20000)
        };

        var projectRepositoryMock = Substitute.For<IProjectRepository>();
        projectRepositoryMock.GetAllAsync().Returns(projects);

        var getAllProjectsQuery = new GetAllProjectsQuery("");
        var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock);

        // Act
        var projectViewModelList =
            await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(projectViewModelList);
        Assert.NotEmpty(projectViewModelList);
        Assert.Equal(projects[0].Id, projectViewModelList[0].Id);
        Assert.Equal(projects[0].Title, projectViewModelList[0].Title);
        Assert.Equal(projects[0].CreatedAt, projectViewModelList[0].CreatedAt);
        Assert.Equal(projects[1].Id, projectViewModelList[1].Id);
        Assert.Equal(projects[1].Title, projectViewModelList[1].Title);
        Assert.Equal(projects[1].CreatedAt, projectViewModelList[1].CreatedAt);
        Assert.Equal(projects.Count, projectViewModelList.Count);

        // Verifica se o método GetAllAsync() foi chamado exatamente uma vez
        await projectRepositoryMock.Received(1).GetAllAsync();
    }
}