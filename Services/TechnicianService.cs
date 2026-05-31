using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;
using ServiceOrders.Api.Services.Interfaces;

namespace ServiceOrders.Api.Services;

public class TechnicianService
    : ITechnicianService
{
    private readonly ITechnicianRepository
        _repository;

    public TechnicianService(
        ITechnicianRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Technician>>
        GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Technician?>
        GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int>
        CreateAsync(Technician technician)
    {
        return await _repository.CreateAsync(
            technician);
    }

    public async Task<bool>
        UpdateAsync(Technician technician)
    {
        return await _repository.UpdateAsync(
            technician);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}