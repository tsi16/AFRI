using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class CurriculumStandard
{
    [Key]
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int GradeLevelId { get; set; }

    [StringLength(50)]
    public string? StandardCode { get; set; }

    public string? Description { get; set; }

    [ForeignKey("GradeLevelId")]
    [InverseProperty("CurriculumStandards")]
    public virtual GradeLevel GradeLevel { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("CurriculumStandards")]
    public virtual Subject Subject { get; set; } = null!;
}
