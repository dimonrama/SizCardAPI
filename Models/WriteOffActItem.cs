using System.ComponentModel.DataAnnotations;

namespace SizCardApi.Models;

/// <summary>
/// Строка акта списания. Данные изделия копируются в строку (снимок на момент списания),
/// поэтому акт остаётся корректным, даже если карточку потом изменят или удалят.
/// SizCardId — обычное число, а не внешний ключ: удаление карточки не блокируется актом.
/// </summary>
public class WriteOffActItem
{
    public int Id { get; set; }

    public int WriteOffActId { get; set; }

    /// <summary>Id карточки СИЗ на момент списания.</summary>
    public int SizCardId { get; set; }

    public WriteOffReason Reason { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? InventoryNumber { get; set; }

    [MaxLength(50)]
    public string? BatchNumber { get; set; }

    [MaxLength(50)]
    public string? SerialNumber { get; set; }

    [MaxLength(20)]
    public string? Size { get; set; }

    [MaxLength(100)]
    public string? StorageLocation { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    [MaxLength(150)]
    public string? Owner { get; set; }
}
