using SizCardApi.DTOs;
using SizCardApi.Models;

namespace SizCardApi.Services;


public interface ISizCardService
{
    Task<List<SizCard>> GetAllAsync(SizStatus? status = null);
    Task<List<SizCard>> GetExpiringSoonAsync();
    Task<SizCard?> GetByIdAsync(int id);
    Task<SizCard> CreateAsync(SizCardUpsertDto dto);
    Task<bool> UpdateAsync(int id, SizCardUpsertDto dto);
    Task<bool> ChangeStatusAsync(int id, SizStatus status);
    Task<bool> DeleteAsync(int id);
}
