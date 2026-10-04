using System.ComponentModel.DataAnnotations;
using SizCardApi.Models;

namespace SizCardApi.DTOs;

/// <summary>Данные для создания акта списания.</summary>
public class CreateWriteOffActDto
{
    /// <summary>Дата акта. Если не указана — сегодняшняя.</summary>
    public DateOnly? ActDate { get; set; }

    [MaxLength(150)]
    public string? ChairmanName { get; set; }

    [MaxLength(500)]
    public string? CommissionMembers { get; set; }

    [MaxLength(500)]
    public string? Note { get; set; }

    /// <summary>
    /// Id карточек, которые включить в акт. Если список пуст или не указан —
    /// в акт попадают ВСЕ карточки, подлежащие списанию.
    /// </summary>
    public List<int>? CardIds { get; set; }
}

/// <summary>Карточка СИЗ, подлежащая списанию, и причина.</summary>
public record WriteOffCandidateDto(
    int Id,
    string Name,
    string? InventoryNumber,
    string? BatchNumber,
    DateOnly? ExpiryDate,
    string? StorageLocation,
    string? Owner,
    SizStatus Status,
    WriteOffReason Reason);
