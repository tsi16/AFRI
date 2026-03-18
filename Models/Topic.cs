using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class Topic
{
    [Key]
    public int Id { get; set; }

    public int SubjectId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public int? EstimatedHours { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Topic")]
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    [InverseProperty("Topic")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    [InverseProperty("Topic")]
    public virtual ICollection<TopicMaterial> TopicMaterials { get; set; } = new List<TopicMaterial>();

    [InverseProperty("Topic")]
    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; } = new List<StudentQuestion>();

    [ForeignKey("SubjectId")]
    [InverseProperty("Topics")]
    public virtual Subject Subject { get; set; } = null!;
}
