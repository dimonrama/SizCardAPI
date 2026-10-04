using SizCardApi.Models;

namespace SizCardApi.Repositories;

public interface IWriteOffRepository
{
    /// <summary>
    /// Карточки, подлежащие списанию: ещё не списаны И (срок годности истёк ИЛИ заключение "Не годен").
    /// Если ids задан — только среди указанных. Сущности отслеживаются контекстом.
    /// </summary>
    Task<List<SizCard>> GetCandidatesAsync(List<int>? ids = null);

    Task<int> CountActsInYearAsync(int year);
    Task AddActAsync(WriteOffAct act);
    Task<List<WriteOffAct>> GetActsAsync();
    Task<WriteOffAct?> GetActByIdAsync(int id);
    Task<bool> SaveChangesAsync();
}
