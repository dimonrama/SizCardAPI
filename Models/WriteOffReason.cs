namespace SizCardApi.Models;

/// <summary>
/// Причина списания СИЗ в акте.
/// </summary>
public enum WriteOffReason
{
    /// <summary>Истёк срок годности.</summary>
    Expired = 0,

    /// <summary>Признано непригодным (заключение "Не годен").</summary>
    Unfit = 1
}

/// <summary>
/// Допустимые значения поля SizCard.Conclusion (оно хранится строкой).
/// </summary>
public static class SizConclusion
{
    public const string Fit = "Годен";
    public const string NeedsRefresh = "Требует освежения";
    public const string Unfit = "Не годен";
}
