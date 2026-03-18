using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

[Index("Code", Name = "UQ__Language__A25C5AA7A145BE30", IsUnique = true)]
public partial class Language
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string Code { get; set; } = null!;

    [Column("IsRTL")]
    public bool? IsRtl { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Language")]
    public virtual ICollection<LessonTranslation> LessonTranslations { get; set; } = new List<LessonTranslation>();

    [InverseProperty("Language")]
    public virtual ICollection<StudentQuestion> StudentQuestions { get; set; } = new List<StudentQuestion>();

    [InverseProperty("PreferredLanguage")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
