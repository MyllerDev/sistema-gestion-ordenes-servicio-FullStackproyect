using ServiceOrders.Api.Models;

namespace ServiceOrders.Api.Services.Interfaces;

public interface ITechnicianService
{
    Task<IEnumerable<Technician>> GetAllAsync();

    Task<Technician?> GetByIdAsync(int id);

    Task<int> CreateAsync(Technician technician);

    Task<bool> UpdateAsync(Technician technician);

    Task<bool> DeleteAsync(int id);
}