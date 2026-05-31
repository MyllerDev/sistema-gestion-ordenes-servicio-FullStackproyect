using ServiceOrders.Api.Models;

namespace ServiceOrders.Api.Repositories.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client?> GetByIdAsync(int id);

    Task<Client?> GetByDocumentAsync(string document);

    Task<int> CreateAsync(Client client);

    Task<bool> UpdateAsync(Client client);

    Task<bool> DeleteAsync(int id);
}