using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceOrders.Api.DTOs;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Services.Interfaces;

namespace ServiceOrders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;

    public ClientsController(
        IClientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var client =
            await _service.GetByIdAsync(id);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateClientDto dto)
    {
        try
        {
            var client = new Client
            {
                FullName = dto.FullName,
                DocumentNumber = dto.DocumentNumber,
                Address = dto.Address,
                Phone = dto.Phone
            };

            var id =
                await _service.CreateAsync(
                    client);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                client);
        }
        catch (Exception ex)
        {
            return BadRequest(
                new
                {
                    Message = ex.Message
                });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateClientDto dto)
    {
        var client = new Client
        {
            Id = id,
            FullName = dto.FullName,
            DocumentNumber = dto.DocumentNumber,
            Address = dto.Address,
            Phone = dto.Phone
        };

        var updated =
            await _service.UpdateAsync(
                client);

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