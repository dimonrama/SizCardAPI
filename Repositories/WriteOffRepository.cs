using Microsoft.EntityFrameworkCore;
using SizCardApi.Data;
using SizCardApi.Models;

namespace SizCardApi.Repositories;

public class WriteOffRepository : IWriteOffRepository
{
    private readonly AppDbContext _db;

    public WriteOffRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<SizCard>> GetCandidatesAsync(List<int>? ids = null)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var query = _db.SizCards.Where(c =>
            c.Status != SizStatus.WrittenOff &&
            ((c.ExpiryDate != null && c.ExpiryDate < today) ||
             c.Conclusion == SizConclusion.Unfit));

        if (ids is { Count: > 0 })
            query = query.Where(c => ids.Contains(c.Id));

        return await query
            .OrderBy(c => c.ExpiryDate)
            .ThenBy(c => c.Id)
            .ToListAsync();
    }

    public async Task<int> CountActsInYearAsync(int year)
    {
        var from = new DateOnly(year, 1, 1);
        var to = new DateOnly(year, 12, 31);
        return await _db.WriteOffActs.CountAsync(a => a.ActDate >= from && a.ActDate <= to);
    }

    public async Task AddActAsync(WriteOffAct act) =>
        await _db.WriteOffActs.AddAsync(act);

    public async Task<List<WriteOffAct>> GetActsAsync() =>
        await _db.WriteOffActs
            .AsNoTracking()
            .Include(a => a.Items)
            .OrderByDescending(a => a.Id)
            .ToListAsync();

    public async Task<WriteOffAct?> GetActByIdAsync(int id) =>
        await _db.WriteOffActs
            .AsNoTracking()
            .Include(a => a.Items)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<bool> SaveChangesAsync() =>
        await _db.SaveChangesAsync() > 0;
}
