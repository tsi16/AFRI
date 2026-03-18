using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class StudentLearningPath
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int LearningPathId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? ProgressPercent { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastAccessed { get; set; }

    [ForeignKey("LearningPathId")]
    [InverseProperty("StudentLearningPaths")]
    public virtual LearningPath LearningPath { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentLearningPaths")]
    public virtual User Student { get; set; } = null!;
}
