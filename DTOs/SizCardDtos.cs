using System.ComponentModel.DataAnnotations;
using SizCardApi.Models;

namespace SizCardApi.DTOs;

/// <summary>Данные для создания / полного обновления карточки СИЗ.</summary>
public class SizCardUpsertDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Manufacturer { get; set; }

    [MaxLength(50)]
    public string? BatchNumber { get; set; }

    public DateOnly? ManufactureDate { get; set; }

    [MaxLength(50)]
    public string? SerialNumber { get; set; }

    [MaxLength(20)]
    public string? Size { get; set; }

    public DateOnly? ReceivedDate { get; set; }

    [MaxLength(100)]
    public string? StorageLocation { get; set; }

    [MaxLength(50)]
    public string? InventoryNumber { get; set; }

    public SizStatus Status { get; set; } = SizStatus.InStock;

    public DateOnly? ExpiryDate { get; set; }

    public DateOnly? LastInspectionDate { get; set; }

    [MaxLength(50)]
    public string? Conclusion { get; set; }

    [MaxLength(150)]
    public string? Owner { get; set; }

    [MaxLength(150)]
    public string? NfgoUnit { get; set; }

    public DateOnly? IssueDate { get; set; }
}
