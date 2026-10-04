using Microsoft.AspNetCore.Mvc;
using SizCardApi.DTOs;
using SizCardApi.Models;
using SizCardApi.Services;

namespace SizCardApi.Controllers;

[ApiController]
[Route("api/writeoff")]
public class WriteOffController : ControllerBase
{
    private readonly IWriteOffService _service;

    public WriteOffController(IWriteOffService service)
    {
        _service = service;
    }

    /// <summary>СИЗ, подлежащие списанию: срок годности истёк или заключение "Не годен".</summary>
    [HttpGet("candidates")]
    public async Task<ActionResult<IEnumerable<WriteOffCandidateDto>>> GetCandidates()
    {
        return Ok(await _service.GetCandidatesAsync());
    }

    /// <summary>
    /// Создать акт списания. Без CardIds в акт попадут все подлежащие списанию СИЗ.
    /// Карточки в акте переводятся в статус "Списано".
    /// </summary>
    [HttpPost("acts")]
    public async Task<ActionResult<WriteOffAct>> CreateAct(CreateWriteOffActDto dto)
    {
        try
        {
            var act = await _service.CreateActAsync(dto);
            return CreatedAtAction(nameof(GetActById), new { id = act.Id }, act);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>Список актов (новые сверху).</summary>
    [HttpGet("acts")]
    public async Task<ActionResult<IEnumerable<WriteOffAct>>> GetActs()
    {
        return Ok(await _service.GetActsAsync());
    }

    [HttpGet("acts/{id:int}")]
    public async Task<ActionResult<WriteOffAct>> GetActById(int id)
    {
        var act = await _service.GetActByIdAsync(id);
        return act is null ? NotFound() : Ok(act);
    }
}
