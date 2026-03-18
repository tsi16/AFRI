using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class Quiz
{
    [Key]
    public int Id { get; set; }

    public int TopicId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public int? TimeLimit { get; set; }

    public int? PassingScore { get; set; }

    public bool? IsPremium { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Quiz")]
    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

    [InverseProperty("Quiz")]
    public virtual ICollection<StudentQuizAttempt> StudentQuizAttempts { get; set; } = new List<StudentQuizAttempt>();

    [ForeignKey("TopicId")]
    [InverseProperty("Quizzes")]
    public virtual Topic Topic { get; set; } = null!;
}
