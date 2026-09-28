using SizCardApi.DTOs;
using SizCardApi.Models;
using SizCardApi.Repositories;

namespace SizCardApi.Services;


public class SizCardService : ISizCardService
{
    private readonly ISizCardRepository _repository;

    public SizCardService(ISizCardRepository repository)
    {
        _repository = repository;
    }

    public Task<List<SizCard>> GetAllAsync(SizStatus? status = null) =>
        _repository.GetAllAsync(status);

    public Task<List<SizCard>> GetExpiringSoonAsync() =>
        _repository.GetExpiringSoonAsync();

    public Task<SizCard?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);

    public async Task<SizCard> CreateAsync(SizCardUpsertDto dto)
    {
        var card = MapToEntity(new SizCard(), dto);
        await _repository.AddAsync(card);
        await _repository.SaveChangesAsync();
        return card;
    }

    public async Task<bool> UpdateAsync(int id, SizCardUpsertDto dto)
    {
        var card = await _repository.GetByIdAsync(id);
        if (card is null) return false;

        MapToEntity(card, dto);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangeStatusAsync(int id, SizStatus status)
    {
        var card = await _repository.GetByIdAsync(id);
        if (card is null) return false;

        card.Status = status;
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var card = await _repository.GetByIdAsync(id);
        if (card is null) return false;

        _repository.Remove(card);
        await _repository.SaveChangesAsync();
        return true;
    }

    private static SizCard MapToEntity(SizCard card, SizCardUpsertDto dto)
    {
        card.Name = dto.Name;
        card.Manufacturer = dto.Manufacturer;
        card.BatchNumber = dto.BatchNumber;
        card.ManufactureDate = dto.ManufactureDate;
        card.SerialNumber = dto.SerialNumber;
        card.Size = dto.Size;
        card.ReceivedDate = dto.ReceivedDate;
        card.StorageLocation = dto.StorageLocation;
        card.InventoryNumber = dto.InventoryNumber;
        card.Status = dto.Status;
        card.ExpiryDate = dto.ExpiryDate;
        card.LastInspectionDate = dto.LastInspectionDate;
        card.Conclusion = dto.Conclusion;
        card.Owner = dto.Owner;
        card.NfgoUnit = dto.NfgoUnit;
        card.IssueDate = dto.IssueDate;
        return card;
    }
}
