using DevFreela.Application.Projects.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Projects.Commands.CreateProject;

public class CreateCommentCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Call_AddCommentAsync_Once()
    {
        // Arrange
        var projectRepository = Substitute.For<IProjectRepository>();
        
        var unitOfWorkMock = Substitute.For<IUnitOfWork>();
        unitOfWorkMock.Projects.Returns(projectRepository);

        var createProjectCommand = new CreateProjectCommand
        {
            Title = "Titulo de Teste",
            Description = "Uma descrição Daora",
            TotalCost = 50000,
            IdClient = 1,
            IdFreelancer = 2
        };

        var createProjectCommandHandler = new CreateProjectCommandHandler(unitOfWorkMock);

        // Act
        await createProjectCommandHandler.Handle(createProjectCommand, CancellationToken.None);

        //Assert
        // Verifica se o método AddAsync foi chamado exatamente uma vez com qualquer parametro do tipo Project
        await unitOfWorkMock.Projects.Received(1).AddAsync(Arg.Any<Project>());

        //Verifica se o método AddAsync foi chamado exatamente uma vez com os argumentos corretos
        await unitOfWorkMock.Projects
            .Received(1)
            .AddAsync(Arg.Is<Project>(p =>
                p.Title == createProjectCommand.Title &&
                p.Description == createProjectCommand.Description &&
                p.TotalCost == createProjectCommand.TotalCost)
            );

        // Verifica se o método StartAsync não foi chamado
        await unitOfWorkMock.Projects.DidNotReceive().StartAsync(Arg.Any<Project>());
    }
}