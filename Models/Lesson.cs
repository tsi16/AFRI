using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class Lesson
{
    [Key]
    public int Id { get; set; }

    public int TopicId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Content { get; set; }

    [StringLength(50)]
    public string? DifficultyLevel { get; set; }

    public bool? IsPremium { get; set; }

    public bool? IsDownloadable { get; set; }

    public bool? IsPublished { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Lesson")]
    public virtual ICollection<ContentReviewAssignment> ContentReviewAssignments { get; set; } = new List<ContentReviewAssignment>();

    [InverseProperty("Lesson")]
    public virtual ICollection<DownloadedLesson> DownloadedLessons { get; set; } = new List<DownloadedLesson>();

    [InverseProperty("Lesson")]
    public virtual ICollection<LessonTranslation> LessonTranslations { get; set; } = new List<LessonTranslation>();

    [InverseProperty("Lesson")]
    public virtual ICollection<StudentProgress> StudentProgresses { get; set; } = new List<StudentProgress>();

    [InverseProperty("Lesson")]
    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; } = new List<StudentQuestion>();

    [ForeignKey("TopicId")]
    [InverseProperty("Lessons")]
    public virtual Topic Topic { get; set; } = null!;
}
