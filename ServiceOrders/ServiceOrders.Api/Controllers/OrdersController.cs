using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceOrders.Api.DTOs;
using ServiceOrders.Api.Models;
using ServiceOrders.Api.Services.Interfaces;

namespace ServiceOrders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(
        IOrderService service)
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
        var order =
            await _service.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderDto dto)
    {
        try
        {
            var order = new ServiceOrder
            {
                Description = dto.Description,
                TechnicianId = dto.TechnicianId,
                ClientId = dto.ClientId
            };

            var id =
                await _service.CreateAsync(
                    order);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                order);
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
        [FromBody] UpdateOrderDto dto)
    {
        try
        {
            var order = new ServiceOrder
            {
                Id = id,
                Description = dto.Description,
                TechnicianId = dto.TechnicianId,
                ClientId = dto.ClientId
            };

            var updated =
                await _service.UpdateAsync(
                    order);

            if (!updated)
                return NotFound();

            return NoContent();
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

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> ChangeStatus(
        int id,
        [FromBody] ChangeOrderStatusDto dto)
    {
        var result =
            await _service.ChangeStatusAsync(
                id,
                (OrderStatus)dto.Status);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPost("filter")]
    public async Task<IActionResult> Filter(
        [FromBody] OrderFilterDto filter)
    {
        var result =
            await _service.FilterAsync(filter);

        return Ok(result);
    }
}