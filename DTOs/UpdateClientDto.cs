namespace ServiceOrders.Api.DTOs;

public class UpdateClientDto
{
    public string FullName { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
}