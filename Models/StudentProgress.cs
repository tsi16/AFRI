using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

[Table("StudentProgress")]
public partial class StudentProgress
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int LessonId { get; set; }

    [StringLength(50)]
    public string? CompletionStatus { get; set; }

    public int? TimeSpentMinutes { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Score { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("StudentProgresses")]
    public virtual Lesson Lesson { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentProgresses")]
    public virtual User Student { get; set; } = null!;
}
