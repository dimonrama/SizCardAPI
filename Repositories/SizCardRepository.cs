using Microsoft.EntityFrameworkCore;
using SizCardApi.Data;
using SizCardApi.Models;

namespace SizCardApi.Repositories;

public class SizCardRepository : ISizCardRepository
{
    private readonly AppDbContext _db;

    public SizCardRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<SizCard>> GetAllAsync(SizStatus? status = null)
    {
        var query = _db.SizCards.AsQueryable();
        if (status is not null)
            query = query.Where(c => c.Status == status);

        return await query.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<List<SizCard>> GetExpiringSoonAsync()
    {
        var threshold = DateOnly.FromDateTime(DateTime.Today.AddMonths(6));
        return await _db.SizCards
            .Where(c => c.ExpiryDate != null && c.ExpiryDate <= threshold)
            .OrderBy(c => c.ExpiryDate)
            .ToListAsync();
    }

    public async Task<SizCard?> GetByIdAsync(int id) =>
        await _db.SizCards.FindAsync(id);

    public async Task AddAsync(SizCard card) =>
        await _db.SizCards.AddAsync(card);

    public void Remove(SizCard card) =>
        _db.SizCards.Remove(card);

    public async Task<bool> SaveChangesAsync() =>
        await _db.SaveChangesAsync() > 0;
}
