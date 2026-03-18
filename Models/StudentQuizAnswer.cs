using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class StudentQuizAnswer
{
    [Key]
    public int Id { get; set; }

    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int? SelectedOptionId { get; set; }

    public string? AnswerText { get; set; }

    [ForeignKey("AttemptId")]
    [InverseProperty("StudentQuizAnswers")]
    public virtual StudentQuizAttempt Attempt { get; set; } = null!;

    [ForeignKey("QuestionId")]
    [InverseProperty("StudentQuizAnswers")]
    public virtual QuizQuestion Question { get; set; } = null!;

    [ForeignKey("SelectedOptionId")]
    [InverseProperty("StudentQuizAnswers")]
    public virtual QuizOption? SelectedOption { get; set; }
}
