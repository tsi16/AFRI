using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class LeaderboardScore
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int? TotalPoints { get; set; }

    public int? Rank { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("LeaderboardScores")]
    public virtual User Student { get; set; } = null!;
}
