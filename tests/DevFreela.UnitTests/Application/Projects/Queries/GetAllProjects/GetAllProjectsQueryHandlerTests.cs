using DevFreela.Application.Projects.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_ProjectViewModelList()
    {
        // Arrange
        var projectsPagination = new PaginationResult<Project>
        {
            Data = new List<Project>
            {
                new Project("Nome Do Teste 1", "Descricao De Teste 1", 1, 2, 10000),
                new Project("Nome Do Teste 2", "Descricao De Teste 2", 1, 2, 20000)
            }
        };

        const string query = "";
        const int page = 1;

        var projectRepositoryMock = Substitute.For<IProjectRepository>();
        projectRepositoryMock.GetAllAsync(query, page).Returns(projectsPagination);

        var getAllProjectsQuery = new GetAllProjectsQuery { Page = page, Query = query };
        var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock);

        // Act
        var projectVMPagination =
            await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, CancellationToken.None);

        // Assert
        Assert.NotNull(projectVMPagination.Data);
        Assert.NotEmpty(projectVMPagination.Data);
        Assert.Equal(projectsPagination.Data[0].Id, projectVMPagination.Data[0].Id);
        Assert.Equal(projectsPagination.Data[0].Title, projectVMPagination.Data[0].Title);
        Assert.Equal(projectsPagination.Data[0].CreatedAt, projectVMPagination.Data[0].CreatedAt);
        Assert.Equal(projectsPagination.Data[1].Id, projectVMPagination.Data[1].Id);
        Assert.Equal(projectsPagination.Data[1].Title, projectVMPagination.Data[1].Title);
        Assert.Equal(projectsPagination.Data[1].CreatedAt, projectVMPagination.Data[1].CreatedAt);
        Assert.Equal(projectsPagination.Data.Count, projectVMPagination.Data.Count);

        // Verifica se o método GetAllAsync() foi chamado exatamente uma vez
        await projectRepositoryMock.Received(1).GetAllAsync(query, page);
    }
}