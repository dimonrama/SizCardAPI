using SizCardApi.DTOs;
using SizCardApi.Models;

namespace SizCardApi.Services;

public interface IWriteOffService
{
    /// <summary>Карточки, которые подлежат списанию прямо сейчас.</summary>
    Task<List<WriteOffCandidateDto>> GetCandidatesAsync();

    /// <summary>
    /// Создаёт акт и переводит включённые карточки в статус "Списано".
    /// Бросает InvalidOperationException, если списывать нечего или выбраны неподходящие карточки.
    /// </summary>
    Task<WriteOffAct> CreateActAsync(CreateWriteOffActDto dto);

    Task<List<WriteOffAct>> GetActsAsync();
    Task<WriteOffAct?> GetActByIdAsync(int id);
}
