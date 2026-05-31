using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;
using ServiceOrders.Api.Services.Interfaces;

namespace ServiceOrders.Api.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository
        _repository;

    public ClientService(
        IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Client>>
        GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client?>
        GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int>
        CreateAsync(Client client)
    {
        var existingClient =
            await _repository.GetByDocumentAsync(
                client.DocumentNumber);

        if (existingClient != null)
        {
            throw new Exception(
                "Document already exists");
        }

        return await _repository.CreateAsync(
            client);
    }

    public async Task<bool>
        UpdateAsync(Client client)
    {
        return await _repository.UpdateAsync(
            client);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}