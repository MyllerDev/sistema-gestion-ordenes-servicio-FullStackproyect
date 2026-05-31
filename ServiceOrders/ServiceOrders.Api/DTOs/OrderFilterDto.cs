namespace ServiceOrders.Api.DTOs;

public class OrderFilterDto
{
    public string? Status { get; set; }

    public string? Technician { get; set; }

    public string? Client { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}