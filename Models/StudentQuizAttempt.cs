using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class StudentQuizAttempt
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int QuizId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AttemptDate { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Score { get; set; }

    public bool? IsPassed { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("StudentQuizAttempts")]
    public virtual Quiz Quiz { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentQuizAttempts")]
    public virtual User Student { get; set; } = null!;

    [InverseProperty("Attempt")]
    public virtual ICollection<StudentQuizAnswer> StudentQuizAnswers { get; set; } = new List<StudentQuizAnswer>();
}
