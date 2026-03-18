using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class LessonTranslation
{
    [Key]
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int LanguageId { get; set; }

    public string? TranslatedContent { get; set; }

    [StringLength(500)]
    public string? VoiceExplanationUrl { get; set; }

    public bool? IsHumanValidated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("LessonTranslations")]
    public virtual Language Language { get; set; } = null!;

    [ForeignKey("LessonId")]
    [InverseProperty("LessonTranslations")]
    public virtual Lesson Lesson { get; set; } = null!;

    [InverseProperty("LessonTranslation")]
    public virtual ICollection<TranslationReviewLog> TranslationReviewLogs { get; set; } = new List<TranslationReviewLog>();
}
