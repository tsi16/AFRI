using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

[Index("Code", Name = "UQ__Subjects__A25C5AA7488CB98D", IsUnique = true)]
public partial class Subject
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string? Code { get; set; }

    public int GradeLevelId { get; set; }

    public string? Description { get; set; }

    public bool? IsCore { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<CurriculumStandard> CurriculumStandards { get; set; } = new List<CurriculumStandard>();

    [ForeignKey("GradeLevelId")]
    [InverseProperty("Subjects")]
    public virtual GradeLevel GradeLevel { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; } = new List<StudentQuestion>();

    [InverseProperty("Subject")]
    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
