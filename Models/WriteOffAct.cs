using System.ComponentModel.DataAnnotations;

namespace SizCardApi.Models;

/// <summary>
/// Акт о списании СИЗ. Создаётся для просроченных / непригодных изделий;
/// при создании акта карточки переводятся в статус "Списано" (выводятся из резерва).
/// </summary>
public class WriteOffAct
{
    public int Id { get; set; }

    /// <summary>Номер акта, напр. "АКТ-2026-0001".</summary>
    [Required, MaxLength(30)]
    public string ActNumber { get; set; } = string.Empty;

    /// <summary>Дата акта.</summary>
    public DateOnly ActDate { get; set; }

    /// <summary>Момент фактического создания записи.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>ФИО председателя комиссии.</summary>
    [MaxLength(150)]
    public string? ChairmanName { get; set; }

    /// <summary>Члены комиссии (ФИО через запятую / с новой строки).</summary>
    [MaxLength(500)]
    public string? CommissionMembers { get; set; }

    /// <summary>Примечание.</summary>
    [MaxLength(500)]
    public string? Note { get; set; }

    public List<WriteOffActItem> Items { get; set; } = new();
}
