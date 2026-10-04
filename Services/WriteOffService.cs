using SizCardApi.DTOs;
using SizCardApi.Models;
using SizCardApi.Repositories;

namespace SizCardApi.Services;

public class WriteOffService : IWriteOffService
{
    private readonly IWriteOffRepository _repository;

    public WriteOffService(IWriteOffRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<WriteOffCandidateDto>> GetCandidatesAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var cards = await _repository.GetCandidatesAsync();

        return cards
            .Select(c => new WriteOffCandidateDto(
                c.Id, c.Name, c.InventoryNumber, c.BatchNumber, c.ExpiryDate,
                c.StorageLocation, c.Owner, c.Status, ResolveReason(c, today)))
            .ToList();
    }

    public async Task<WriteOffAct> CreateActAsync(CreateWriteOffActDto dto)
    {
        var ids = dto.CardIds?.Distinct().ToList();
        var cards = await _repository.GetCandidatesAsync(ids);

        if (cards.Count == 0)
            throw new InvalidOperationException("Нет СИЗ, подлежащих списанию.");

        if (ids is { Count: > 0 } && cards.Count != ids.Count)
            throw new InvalidOperationException(
                "Часть выбранных карточек не подлежит списанию (уже списаны или срок годности не истёк). Обновите список.");

        var today = DateOnly.FromDateTime(DateTime.Today);
        var actDate = dto.ActDate ?? today;
        var sequence = await _repository.CountActsInYearAsync(actDate.Year) + 1;

        var act = new WriteOffAct
        {
            ActNumber = $"АКТ-{actDate.Year}-{sequence:0000}",
            ActDate = actDate,
            CreatedAt = DateTime.Now,
            ChairmanName = dto.ChairmanName,
            CommissionMembers = dto.CommissionMembers,
            Note = dto.Note
        };

        foreach (var card in cards)
        {
            // Причину определяем ДО изменения карточки.
            var reason = ResolveReason(card, today);

            act.Items.Add(new WriteOffActItem
            {
                SizCardId = card.Id,
                Reason = reason,
                Name = card.Name,
                InventoryNumber = card.InventoryNumber,
                BatchNumber = card.BatchNumber,
                SerialNumber = card.SerialNumber,
                Size = card.Size,
                StorageLocation = card.StorageLocation,
                ExpiryDate = card.ExpiryDate,
                Owner = card.Owner
            });

            // Вывод из резерва.
            card.Status = SizStatus.WrittenOff;
            card.Conclusion = SizConclusion.Unfit;
        }

        // Акт и изменения карточек сохраняются одним SaveChanges — это одна транзакция:
        // либо акт создан и все карточки списаны, либо ничего не изменилось.
        await _repository.AddActAsync(act);
        await _repository.SaveChangesAsync();
        return act;
    }

    public Task<List<WriteOffAct>> GetActsAsync() =>
        _repository.GetActsAsync();

    public Task<WriteOffAct?> GetActByIdAsync(int id) =>
        _repository.GetActByIdAsync(id);

    private static WriteOffReason ResolveReason(SizCard card, DateOnly today) =>
        card.ExpiryDate.HasValue && card.ExpiryDate.Value < today
            ? WriteOffReason.Expired
            : WriteOffReason.Unfit;
}
