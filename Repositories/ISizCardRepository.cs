using SizCardApi.Models;

namespace SizCardApi.Repositories;


public interface ISizCardRepository
{
    Task<List<SizCard>> GetAllAsync(SizStatus? status = null);
    Task<List<SizCard>> GetExpiringSoonAsync();
    Task<SizCard?> GetByIdAsync(int id);
    Task AddAsync(SizCard card);
    void Remove(SizCard card);
    Task<bool> SaveChangesAsync();
}
