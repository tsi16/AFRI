using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class QuizQuestion
{
    [Key]
    public int Id { get; set; }
    public int QuizId { get; set; }

    public string? QuestionText { get; set; }

    [StringLength(50)]
    public string? QuestionType { get; set; }

    [StringLength(50)]
    public string? DifficultyLevel { get; set; }

    public int? Marks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("QuizQuestions")]
    public virtual Quiz Quiz { get; set; } = null!;

    [InverseProperty("Question")]
    public virtual ICollection<QuizOption> QuizOptions { get; set; } = new List<QuizOption>();

    [InverseProperty("Question")]
    public virtual ICollection<StudentQuizAnswer> StudentQuizAnswers { get; set; } = new List<StudentQuizAnswer>();
}
