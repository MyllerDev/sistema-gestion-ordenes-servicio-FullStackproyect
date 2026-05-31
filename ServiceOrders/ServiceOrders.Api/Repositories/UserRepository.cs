using Dapper;
using ServiceOrders.Api.Data;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;

namespace ServiceOrders.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public UserRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            SELECT
                Id,
                Username,
                PasswordHash,
                Role
            FROM Users
            WHERE Username = @Username";

        return await connection.QueryFirstOrDefaultAsync<User>(
            sql,
            new { Username = username });
    }
}