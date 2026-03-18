using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class DownloadedLesson
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int LessonId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DownloadDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    public bool? IsSynced { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("DownloadedLessons")]
    public virtual Lesson Lesson { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("DownloadedLessons")]
    public virtual User Student { get; set; } = null!;
}
