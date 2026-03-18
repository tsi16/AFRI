using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

[Table("AIInteractionFeedback")]
public partial class AiinteractionFeedback
{
    [Key]
    public int Id { get; set; }

    [Column("AIResponseId")]
    public int AiresponseId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("AiresponseId")]
    [InverseProperty("AiinteractionFeedbacks")]
    public virtual Airesponse Airesponse { get; set; } = null!;
}
