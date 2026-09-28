namespace SizCardApi.Models;

/// <summary>
/// Статус изделия СИЗ (см. "Пункт №2" методички — поле "Статус").
/// </summary>
public enum SizStatus
{
    /// <summary>На хранении (резерв на складе).</summary>
    InStock = 0,

    /// <summary>Выдано на руки сотруднику.</summary>
    Issued = 1,

    /// <summary>На проверке (в лаборатории).</summary>
    UnderInspection = 2,

    /// <summary>Списано (истёк срок годности / негодно).</summary>
    WrittenOff = 3
}
