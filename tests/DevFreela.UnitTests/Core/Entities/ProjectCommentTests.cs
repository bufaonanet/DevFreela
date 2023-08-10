using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Core.Entities;

public class ProjectCommentTests
{
    [Fact]
    public void Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        string content = "Test comment";
        int idProject = 1;
        int idUser = 2;

        // Act
        var comment = new ProjectComment(content, idProject, idUser);

        // Assert
        Assert.Equal(content, comment.Content);
        Assert.Equal(idProject, comment.IdProject);
        Assert.Equal(idUser, comment.IdUser);
        Assert.NotNull(comment.CreatedAt);
    }
}