using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceOrders.Api.DTOs;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Services.Interfaces;

namespace ServiceOrders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TechniciansController : ControllerBase
{
    private readonly ITechnicianService _service;

    public TechniciansController(
        ITechnicianService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var technicians =
            await _service.GetAllAsync();

        return Ok(technicians);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var technician =
            await _service.GetByIdAsync(id);

        if (technician == null)
            return NotFound();

        return Ok(technician);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTechnicianDto dto)
    {
        var technician = new Technician
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            Specialty = dto.Specialty
        };

        var id =
            await _service.CreateAsync(
                technician);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            technician);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateTechnicianDto dto)
    {
        var technician = new Technician
        {
            Id = id,
            FullName = dto.FullName,
            Phone = dto.Phone,
            Specialty = dto.Specialty
        };

        var updated =
            await _service.UpdateAsync(
                technician);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var deleted =
            await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}