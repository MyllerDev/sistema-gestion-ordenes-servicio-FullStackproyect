using ServiceOrders.Api.Models;

namespace ServiceOrders.Api.Services.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client?> GetByIdAsync(int id);

    Task<int> CreateAsync(Client client);

    Task<bool> UpdateAsync(Client client);

    Task<bool> DeleteAsync(int id);
}