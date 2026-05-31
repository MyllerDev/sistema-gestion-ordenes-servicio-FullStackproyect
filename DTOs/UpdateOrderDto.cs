namespace ServiceOrders.Api.DTOs;

public class UpdateOrderDto
{
    public string Description { get; set; } = string.Empty;

    public int TechnicianId { get; set; }

    public int ClientId { get; set; }
}