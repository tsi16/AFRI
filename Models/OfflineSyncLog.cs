using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class OfflineSyncLog
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SyncDate { get; set; }

    [StringLength(500)]
    public string? DeviceInfo { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("OfflineSyncLogs")]
    public virtual User Student { get; set; } = null!;
}
