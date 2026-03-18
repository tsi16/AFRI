using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class TranslationReviewLog
{
    [Key]
    public int Id { get; set; }

    public int LessonTranslationId { get; set; }

    public int ReviewerUserId { get; set; }

    [StringLength(50)]
    public string? ReviewStatus { get; set; }

    public string? Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewedDate { get; set; }

    [ForeignKey("LessonTranslationId")]
    [InverseProperty("TranslationReviewLogs")]
    public virtual LessonTranslation LessonTranslation { get; set; } = null!;

    [ForeignKey("ReviewerUserId")]
    [InverseProperty("TranslationReviewLogs")]
    public virtual User ReviewerUser { get; set; } = null!;
}
