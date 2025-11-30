using Microsoft.Data.Sqlite;
using Dapper;

namespace TeamGoalTracker.Api.Data;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=teamgoals.db";
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Create Members table
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Members (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Mood TEXT NOT NULL DEFAULT 'neutral'
            )
        ");

        // Create Goals table
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Goals (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                MemberId INTEGER NOT NULL,
                Description TEXT NOT NULL,
                IsCompleted INTEGER NOT NULL DEFAULT 0,
                FOREIGN KEY (MemberId) REFERENCES Members(Id) ON DELETE CASCADE
            )
        ");

        // Create index for faster goal lookups
        connection.Execute(@"
            CREATE INDEX IF NOT EXISTS idx_goals_member ON Goals(MemberId)
        ");

        // Seed data if Members table is empty
        var memberCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Members");
        if (memberCount == 0)
        {
            connection.Execute(@"
                INSERT INTO Members (Name, Mood) VALUES
                    ('Alice', 'happy'),
                    ('Bob', 'neutral'),
                    ('Carol', 'excited'),
                    ('David', 'stressed'),
                    ('Eve', 'neutral')
            ");
        }
    }
}
