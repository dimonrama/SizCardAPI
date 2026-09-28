using Microsoft.AspNetCore.Mvc;
using SizCardApi.DTOs;
using SizCardApi.Models;
using SizCardApi.Services;

namespace SizCardApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizCardsController : ControllerBase
{
    private readonly ISizCardService _service;

    public SizCardsController(ISizCardService service)
    {
        _service = service;
    }

    /// <summary>Список всех карточек. Поддерживает необязательный фильтр по статусу.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SizCard>>> GetAll([FromQuery] SizStatus? status)
    {
        return Ok(await _service.GetAllAsync(status));
    }

    /// <summary>Карточки, у которых срок годности истёк или истекает в ближайшие 6 месяцев.</summary>
    [HttpGet("expiring-soon")]
    public async Task<ActionResult<IEnumerable<SizCard>>> GetExpiringSoon()
    {
        return Ok(await _service.GetExpiringSoonAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SizCard>> GetById(int id)
    {
        var card = await _service.GetByIdAsync(id);
        return card is null ? NotFound() : Ok(card);
    }

    [HttpPost]
    public async Task<ActionResult<SizCard>> Create(SizCardUpsertDto dto)
    {
        var card = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = card.Id }, card);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, SizCardUpsertDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    /// <summary>Точечное изменение статуса (напр. "Выдано на руки" / "Списано").</summary>
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] SizStatus status)
    {
        var updated = await _service.ChangeStatusAsync(id, status);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
