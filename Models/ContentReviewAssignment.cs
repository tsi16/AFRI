using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class ContentReviewAssignment
{
    [Key]
    public int Id { get; set; }

    public int AssignedTeacherId { get; set; }

    public int LessonId { get; set; }

    [StringLength(50)]
    public string? ReviewStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AssignedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewedDate { get; set; }

    [ForeignKey("AssignedTeacherId")]
    [InverseProperty("ContentReviewAssignments")]
    public virtual User AssignedTeacher { get; set; } = null!;

    [ForeignKey("LessonId")]
    [InverseProperty("ContentReviewAssignments")]
    public virtual Lesson Lesson { get; set; } = null!;
}
