using ServiceOrders.Api.DTOs;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Repositories.Interfaces;
using ServiceOrders.Api.Services.Interfaces;

namespace ServiceOrders.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly ITechnicianRepository _technicianRepository;

    private readonly IClientRepository _clientRepository;

    public OrderService(
        IOrderRepository orderRepository,
        ITechnicianRepository technicianRepository,
        IClientRepository clientRepository)
    {
        _orderRepository = orderRepository;

        _technicianRepository = technicianRepository;

        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ServiceOrder>>
        GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<ServiceOrder?>
        GetByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<int>
        CreateAsync(ServiceOrder order)
    {
        var technician =
            await _technicianRepository.GetByIdAsync(
                order.TechnicianId);

        if (technician == null)
        {
            throw new Exception(
                "Technician does not exist");
        }

        var client =
            await _clientRepository.GetByIdAsync(
                order.ClientId);

        if (client == null)
        {
            throw new Exception(
                "Client does not exist");
        }

        order.CreatedDate = DateTime.UtcNow;

        order.Status = OrderStatus.Pending;

        return await _orderRepository.CreateAsync(
            order);
    }

    public async Task<bool>
        UpdateAsync(ServiceOrder order)
    {
        var technician =
            await _technicianRepository.GetByIdAsync(
                order.TechnicianId);

        if (technician == null)
        {
            throw new Exception(
                "Technician does not exist");
        }

        var client =
            await _clientRepository.GetByIdAsync(
                order.ClientId);

        if (client == null)
        {
            throw new Exception(
                "Client does not exist");
        }

        return await _orderRepository.UpdateAsync(
            order);
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        return await _orderRepository.DeleteAsync(
            id);
    }

    public async Task<bool>
        ChangeStatusAsync(
            int id,
            OrderStatus status)
    {
        return await _orderRepository
            .ChangeStatusAsync(id, status);
    }

    public async Task<IEnumerable<dynamic>>
        FilterAsync(OrderFilterDto filter)
    {
        return await _orderRepository
            .FilterAsync(filter);
    }
}