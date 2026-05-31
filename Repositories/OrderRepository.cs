using Dapper;
using ServiceOrders.Api.Data;
using ServiceOrders.Api.DTOs;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;
using System.Text;

namespace ServiceOrders.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public OrderRepository(
        DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ServiceOrder>> GetAllAsync()
    {
        using var connection =
            _connectionFactory.CreateConnection();

        return await connection.QueryAsync<ServiceOrder>(
            "SELECT * FROM ServiceOrders");
    }

    public async Task<ServiceOrder?> GetByIdAsync(int id)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<ServiceOrder>(
            "SELECT * FROM ServiceOrders WHERE Id=@Id",
            new { Id = id });
    }

    public async Task<int> CreateAsync(ServiceOrder order)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO ServiceOrders
            (
                CreatedDate,
                Status,
                Description,
                TechnicianId,
                ClientId
            )
            VALUES
            (
                @CreatedDate,
                @Status,
                @Description,
                @TechnicianId,
                @ClientId
            )
            RETURNING Id";

        return await connection.ExecuteScalarAsync<int>(
            sql,
            order);
    }

    public async Task<bool> UpdateAsync(ServiceOrder order)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE ServiceOrders
            SET
                Description=@Description,
                TechnicianId=@TechnicianId,
                ClientId=@ClientId
            WHERE Id=@Id";

        var rows =
            await connection.ExecuteAsync(
                sql,
                order);

        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var rows =
            await connection.ExecuteAsync(
                "DELETE FROM ServiceOrders WHERE Id=@Id",
                new { Id = id });

        return rows > 0;
    }

    public async Task<bool> ChangeStatusAsync(
        int id,
        OrderStatus status)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var rows =
            await connection.ExecuteAsync(
                @"UPDATE ServiceOrders
                  SET Status=@Status
                  WHERE Id=@Id",
                new
                {
                    Id = id,
                    Status = status.ToString()
                });

        return rows > 0;
    }

    public async Task<IEnumerable<dynamic>> FilterAsync(
        OrderFilterDto filter)
    {
        using var connection =
            _connectionFactory.CreateConnection();

        var sql = new StringBuilder(@"
            SELECT
                o.*,
                t.FullName AS TechnicianName,
                t.Specialty,
                c.FullName AS ClientName,
                c.DocumentNumber
            FROM ServiceOrders o
            INNER JOIN Technicians t
                ON o.TechnicianId = t.Id
            INNER JOIN Clients c
                ON o.ClientId = c.Id
            WHERE 1=1
        ");

        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(filter.Status))
        {
            sql.Append(" AND o.Status = @Status");
            parameters.Add("Status", filter.Status);
        }

        if (!string.IsNullOrWhiteSpace(filter.Technician))
        {
            sql.Append(@"
                AND (
                    t.FullName ILIKE @Technician
                    OR t.Specialty ILIKE @Technician
                )");

            parameters.Add(
                "Technician",
                $"%{filter.Technician}%");
        }

        if (!string.IsNullOrWhiteSpace(filter.Client))
        {
            sql.Append(@"
                AND (
                    c.FullName ILIKE @Client
                    OR c.DocumentNumber ILIKE @Client
                )");

            parameters.Add(
                "Client",
                $"%{filter.Client}%");
        }

        if (filter.StartDate.HasValue)
        {
            sql.Append(
                " AND o.CreatedDate >= @StartDate");

            parameters.Add(
                "StartDate",
                filter.StartDate);
        }

        if (filter.EndDate.HasValue)
        {
            sql.Append(
                " AND o.CreatedDate <= @EndDate");

            parameters.Add(
                "EndDate",
                filter.EndDate);
        }

        return await connection.QueryAsync(
            sql.ToString(),
            parameters);
    }
}