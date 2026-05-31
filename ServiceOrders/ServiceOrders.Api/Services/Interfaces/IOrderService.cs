using ServiceOrders.Api.DTOs;
using ServiceOrders.Api.Models;

namespace ServiceOrders.Api.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<ServiceOrder>> GetAllAsync();

    Task<ServiceOrder?> GetByIdAsync(int id);

    Task<int> CreateAsync(ServiceOrder order);

    Task<bool> UpdateAsync(ServiceOrder order);

    Task<bool> DeleteAsync(int id);

    Task<bool> ChangeStatusAsync(
        int id,
        OrderStatus status);

    Task<IEnumerable<dynamic>> FilterAsync(
        OrderFilterDto filter);
}