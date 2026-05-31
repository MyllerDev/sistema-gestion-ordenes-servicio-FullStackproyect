namespace ServiceOrders.Api.DTOs;

public class UpdateTechnicianDto
{
    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Specialty { get; set; } = string.Empty;
}