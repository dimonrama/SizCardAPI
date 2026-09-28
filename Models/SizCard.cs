using System.ComponentModel.DataAnnotations;

namespace SizCardApi.Models;

/// <summary>
/// Электронная карточка учёта средства индивидуальной защиты (СИЗ).
/// Поля соответствуют структуре из "Пункт №2" методички ГО:
/// паспортные данные, складские данные, контроль сроков, персонализация.
/// </summary>
public class SizCard
{
    public int Id { get; set; }

    // --- 1. Паспортные данные ---

    /// <summary>Наименование изделия, напр. "Противогаз ГП-7".</summary>
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>Завод-изготовитель.</summary>
    [MaxLength(200)]
    public string? Manufacturer { get; set; }

    /// <summary>Номер партии.</summary>
    [MaxLength(50)]
    public string? BatchNumber { get; set; }

    /// <summary>Дата изготовления.</summary>
    public DateOnly? ManufactureDate { get; set; }

    /// <summary>Заводской номер изделия (если есть).</summary>
    [MaxLength(50)]
    public string? SerialNumber { get; set; }

    /// <summary>Рост/размер (1,2,3,4 — для противогазов/костюмов).</summary>
    [MaxLength(20)]
    public string? Size { get; set; }

    // --- 2. Логистические и складские данные ---

    /// <summary>Дата поступления на склад.</summary>
    public DateOnly? ReceivedDate { get; set; }

    /// <summary>Место хранения (номер склада/стеллажа/ячейки).</summary>
    [MaxLength(100)]
    public string? StorageLocation { get; set; }

    /// <summary>Внутренний инвентарный номер организации.</summary>
    [MaxLength(50)]
    public string? InventoryNumber { get; set; }

    public SizStatus Status { get; set; } = SizStatus.InStock;

    // --- 3. Техническое состояние и сроки ---

    /// <summary>Дата истечения срока годности (критичное поле).</summary>
    public DateOnly? ExpiryDate { get; set; }

    /// <summary>Дата последней лабораторной проверки.</summary>
    public DateOnly? LastInspectionDate { get; set; }

    /// <summary>Заключение о годности: "Годен" / "Требует освежения" / "Не годен".</summary>
    [MaxLength(50)]
    public string? Conclusion { get; set; }

    // --- 4. Персонализация ---

    /// <summary>ФИО владельца, если выдано на руки.</summary>
    [MaxLength(150)]
    public string? Owner { get; set; }

    /// <summary>Принадлежность к НФГО (звено связи, пост наблюдения и т.п.).</summary>
    [MaxLength(150)]
    public string? NfgoUnit { get; set; }

    /// <summary>Дата выдачи владельцу.</summary>
    public DateOnly? IssueDate { get; set; }

    /// <summary>
    /// Вычисляемое свойство: true, если срок годности истёк или истекает
    /// в ближайшие 6 месяцев (см. требование "Триггер Критический срок").
    /// </summary>
    public bool IsExpiringSoon =>
        ExpiryDate.HasValue &&
        ExpiryDate.Value <= DateOnly.FromDateTime(DateTime.Today.AddMonths(6));
}
