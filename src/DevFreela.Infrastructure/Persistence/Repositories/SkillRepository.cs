using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly string _connectionString;

    public SkillRepository(DevFreelaDbContext dbContext)
    {
        _connectionString = dbContext.GetConnectionString();
    }
    
    public async Task<List<SkillDTO>> GetAllAsync()
    {
        await using var sqlConnection = new SqlConnection(_connectionString);
        sqlConnection.Open();

        const string script = "SELECT Id, Description FROM Skills";

        var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

        return skills.ToList();

        // COM EF CORE
        //var skills = _dbContext.Skills;

        //var skillsViewModel = skills
        //    .Select(s => new SkillViewModel(s.Id, s.Description))
        //    .ToList();

        //return skillsViewModel;
    }
}