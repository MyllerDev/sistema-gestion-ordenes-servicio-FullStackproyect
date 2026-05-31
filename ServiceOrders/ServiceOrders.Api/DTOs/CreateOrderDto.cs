namespace ServiceOrders.Api.DTOs;

public class CreateOrderDto
{
    public string Description { get; set; } = string.Empty;

    public int TechnicianId { get; set; }

    public int ClientId { get; set; }
}