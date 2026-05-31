using Dapper;
using ServiceOrders.Api.Data;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;

namespace ServiceOrders.Api.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public ClientRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryAsync<Client>(
            "SELECT * FROM Clients");
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Client>(
            "SELECT * FROM Clients WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<Client?> GetByDocumentAsync(string document)
    {
        using var connection = _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Client>(
            "SELECT * FROM Clients WHERE DocumentNumber = @Document",
            new { Document = document });
    }

    public async Task<int> CreateAsync(Client client)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO Clients
            (
                FullName,
                DocumentNumber,
                Address,
                Phone
            )
            VALUES
            (
                @FullName,
                @DocumentNumber,
                @Address,
                @Phone
            )
            RETURNING Id";

        return await connection.ExecuteScalarAsync<int>(
            sql,
            client);
    }

    public async Task<bool> UpdateAsync(Client client)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE Clients
            SET
                FullName = @FullName,
                DocumentNumber = @DocumentNumber,
                Address = @Address,
                Phone = @Phone
            WHERE Id = @Id";

        var rows = await connection.ExecuteAsync(
            sql,
            client);

        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();

        var rows = await connection.ExecuteAsync(
            "DELETE FROM Clients WHERE Id = @Id",
            new { Id = id });

        return rows > 0;
    }
}