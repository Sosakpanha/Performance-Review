using Microsoft.Data.Sqlite;
using Dapper;
using TeamGoalTracker.Api.Models;
using TeamGoalTracker.Api.Repositories.Interfaces;

namespace TeamGoalTracker.Api.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly string _connectionString;

    public GoalRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=teamgoals.db";
    }

    public async Task<IEnumerable<Goal>> GetByMemberIdAsync(int memberId)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryAsync<Goal>(
            "SELECT Id, MemberId, Description, IsCompleted FROM Goals WHERE MemberId = @MemberId ORDER BY Id",
            new { MemberId = memberId });
    }

    public async Task<IEnumerable<Goal>> GetAllAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryAsync<Goal>(
            "SELECT Id, MemberId, Description, IsCompleted FROM Goals ORDER BY MemberId, Id");
    }

    public async Task<Goal?> GetByIdAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Goal>(
            "SELECT Id, MemberId, Description, IsCompleted FROM Goals WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<Goal> CreateAsync(int memberId, string description)
    {
        using var connection = new SqliteConnection(_connectionString);
        var id = await connection.ExecuteScalarAsync<int>(
            @"INSERT INTO Goals (MemberId, Description, IsCompleted) VALUES (@MemberId, @Description, 0);
              SELECT last_insert_rowid();",
            new { MemberId = memberId, Description = description });

        return new Goal
        {
            Id = id,
            MemberId = memberId,
            Description = description,
            IsCompleted = false
        };
    }

    public async Task ToggleAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.ExecuteAsync(
            "UPDATE Goals SET IsCompleted = NOT IsCompleted WHERE Id = @Id",
            new { Id = id });
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.ExecuteAsync(
            "DELETE FROM Goals WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<(int total, int completed)> GetStatsAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        var total = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Goals");
        var completed = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Goals WHERE IsCompleted = 1");
        return (total, completed);
    }
}
