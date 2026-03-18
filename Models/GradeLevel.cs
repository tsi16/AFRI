using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class GradeLevel
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int OrderNumber { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("GradeLevel")]
    public virtual ICollection<CurriculumStandard> CurriculumStandards { get; set; } = new List<CurriculumStandard>();

    [InverseProperty("GradeLevel")]
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    [InverseProperty("GradeLevel")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
