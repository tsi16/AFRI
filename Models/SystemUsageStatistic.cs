using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class SystemUsageStatistic
{
    [Key]
    public int Id { get; set; }

    public DateOnly StatisticDate { get; set; }

    public int? ActiveUsers { get; set; }

    public int? LessonsAccessed { get; set; }

    public int? QuizzesAttempted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }
}
