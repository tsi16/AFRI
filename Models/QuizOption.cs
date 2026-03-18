using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class QuizOption
{
    [Key]
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string? OptionText { get; set; }

    public bool? IsCorrect { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("QuizOptions")]
    public virtual QuizQuestion Question { get; set; } = null!;

    [InverseProperty("SelectedOption")]
    public virtual ICollection<StudentQuizAnswer> StudentQuizAnswers { get; set; } = new List<StudentQuizAnswer>();
}
