using Microsoft.Data.Sqlite;
using Dapper;
using TeamGoalTracker.Api.Models;
using TeamGoalTracker.Api.Repositories.Interfaces;

namespace TeamGoalTracker.Api.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly string _connectionString;

    public MemberRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=teamgoals.db";
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryAsync<Member>(
            "SELECT Id, Name, Mood FROM Members ORDER BY Name");
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Member>(
            "SELECT Id, Name, Mood FROM Members WHERE Id = @Id",
            new { Id = id });
    }

    public async Task UpdateMoodAsync(int id, string mood)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.ExecuteAsync(
            "UPDATE Members SET Mood = @Mood WHERE Id = @Id",
            new { Id = id, Mood = mood });
    }

    public async Task<Dictionary<string, int>> GetMoodCountsAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        var results = await connection.QueryAsync<(string Mood, int Count)>(
            "SELECT Mood, COUNT(*) as Count FROM Members GROUP BY Mood");
        return results.ToDictionary(r => r.Mood, r => r.Count);
    }
}
