namespace ServiceOrders.Api.Models;

public class ServiceOrder
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public OrderStatus Status { get; set; }

    public string Description { get; set; } = string.Empty;

    public int TechnicianId { get; set; }

    public int ClientId { get; set; }
}