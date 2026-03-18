using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class StudentActivityLog
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    [StringLength(100)]
    public string? ActivityType { get; set; }

    public int? ReferenceId { get; set; }

    [StringLength(500)]
    public string? DeviceInfo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActivityDate { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentActivityLogs")]
    public virtual User Student { get; set; } = null!;
}
