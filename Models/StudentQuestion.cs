using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class StudentQuestion
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int? TopicId { get; set; }

    public int? LessonId { get; set; }

    public int? LanguageId { get; set; }

    public string? QuestionText { get; set; }

    public bool? IsVoiceInput { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("StudentQuestion")]
    public virtual ICollection<Airesponse> Airesponses { get; set; } = new List<Airesponse>();

    [ForeignKey("LanguageId")]
    [InverseProperty("StudentQuestions")]
    public virtual Language? Language { get; set; }

    [ForeignKey("LessonId")]
    [InverseProperty("StudentQuestions")]
    public virtual Lesson? Lesson { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentQuestions")]
    public virtual User Student { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("StudentQuestions")]
    public virtual Subject Subject { get; set; } = null!;

    [ForeignKey("TopicId")]
    [InverseProperty("StudentQuestions")]
    public virtual Topic? Topic { get; set; }
}
