using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionString;

    public ProjectRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
        _connectionString = _dbContext.GetConnectionString();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _dbContext.Projects
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Project>? GetDetailsByIdAsync(int id)
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == id);

        return projects;
    }

    public async Task<Project>? GetByIdAsync(int id)
    {
        var project = await _dbContext.Projects
            .SingleOrDefaultAsync(p => p.Id == id);
        return project;
    }

    public async Task AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await SaveChangesAsync();
    }

    public async Task StartAsync(Project project)
    {
        await using var sqlConnection = new SqlConnection(_connectionString);
        sqlConnection.Open();

        const string script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

        await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
    }

    public async Task AddCommentAsync(ProjectComment projectComment)
    {
        await _dbContext.ProjectComments.AddAsync(projectComment);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}