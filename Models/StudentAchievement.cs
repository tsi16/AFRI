using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class StudentAchievement
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int AchievementId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AchievedDate { get; set; }

    [ForeignKey("AchievementId")]
    [InverseProperty("StudentAchievements")]
    public virtual Achievement Achievement { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentAchievements")]
    public virtual User Student { get; set; } = null!;
}
