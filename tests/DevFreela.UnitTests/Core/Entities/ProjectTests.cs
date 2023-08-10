using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core.Entities;

public class ProjectTests
{
    [Fact]
    public void Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        var title = "Test Project";
        var description = "This is a test project.";
        var idClient = 1;
        var idFreelancer = 2;
        var totalCost = 100.00M;

        // Act
        var project = new Project(title, description, idClient, idFreelancer, totalCost);

        // Assert
        Assert.Equal(title, project.Title);
        Assert.Equal(description, project.Description);
        Assert.Equal(idClient, project.IdClient);
        Assert.Equal(idFreelancer, project.IdFreelancer);
        Assert.Equal(totalCost, project.TotalCost);
        Assert.Equal(ProjectStatusEnum.Created, project.Status);
        Assert.Empty(project.Comments);
    }
    
    [Fact]
    public void Start_Should_Set_Status_To_InProgress_If_Created()
    {
        // Arrange
        var project = new Project("Test", "Description", 1, 2, 100.00M);

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);
    }
    
    [Fact]
    public void Cancel_Should_Set_Status_To_Cancelled_If_InProgress()
    {
        // Arrange
        var project = new Project("Test", "Description", 1, 2, 100.00M);
        project.Start();  // Set status to InProgress

        // Act
        project.Cancel();

        // Assert
        Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
    }

    [Fact]
    public void Finish_Should_Set_Status_To_Finished_If_InProgress()
    {
        // Arrange
        var project = new Project("Test", "Description", 1, 2, 100.00M);
        project.Start();  // Set status to InProgress

        // Act
        project.Finish();

        // Assert
        Assert.Equal(ProjectStatusEnum.Finished, project.Status);
        Assert.NotNull(project.FinishedAt);
    }
    
    [Fact]
    public void Update_Should_Update_Properties_Correctly()
    {
        // Arrange
        var project = new Project("Test", "Description", 1, 2, 100.00M);
        string newTitle = "Updated Title";
        string newDescription = "Updated Description";
        decimal newTotalCost = 150.00M;

        // Act
        project.Update(newTitle, newDescription, newTotalCost);

        // Assert
        Assert.Equal(newTitle, project.Title);
        Assert.Equal(newDescription, project.Description);
        Assert.Equal(newTotalCost, project.TotalCost);
    }
    
    [Fact]
    public void TestIfProjectStartWorks()
    {
        var project = new Project("Nome De Teste", "Descricao de Teste", 1, 2, 10000);
        
        Assert.Equal(ProjectStatusEnum.Created, project.Status);
        Assert.Null(project.StartedAt);
        
        Assert.NotNull(project.Title);
        Assert.NotEmpty(project.Title);

        Assert.NotNull(project.Description);
        Assert.NotEmpty(project.Description);
        
        project.Start();
        
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);
    }
}