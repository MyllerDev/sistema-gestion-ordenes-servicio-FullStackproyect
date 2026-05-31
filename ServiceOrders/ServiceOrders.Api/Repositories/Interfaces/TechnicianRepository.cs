using Dapper;
using ServiceOrders.Api.Data;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;

namespace ServiceOrders.Api.Repositories;

public class TechnicianRepository : ITechnicianRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public TechnicianRepository(
        DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Technician>> GetAllAsync()
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql = "SELECT * FROM Technicians";

        return await connection.QueryAsync<Technician>(sql);
    }

    public async Task<Technician?> GetByIdAsync(int id)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql =
            "SELECT * FROM Technicians WHERE Id=@Id";

        return await connection.QueryFirstOrDefaultAsync<Technician>(
            sql,
            new { Id = id });
    }

    public async Task<int> CreateAsync(
        Technician technician)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO Technicians
            (
                FullName,
                Phone,
                Specialty
            )
            VALUES
            (
                @FullName,
                @Phone,
                @Specialty
            )
            RETURNING Id";

        return await connection.ExecuteScalarAsync<int>(
            sql,
            technician);
    }

    public async Task<bool> UpdateAsync(
        Technician technician)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE Technicians
            SET
                FullName=@FullName,
                Phone=@Phone,
                Specialty=@Specialty
            WHERE Id=@Id";

        var rows =
            await connection.ExecuteAsync(
                sql,
                technician);

        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql =
            "DELETE FROM Technicians WHERE Id=@Id";

        var rows =
            await connection.ExecuteAsync(
                sql,
                new { Id = id });

        return rows > 0;
    }
}