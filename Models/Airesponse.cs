using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

[Table("AIResponses")]
public partial class Airesponse
{
    [Key]
    public int Id { get; set; }

    public int StudentQuestionId { get; set; }

    public string? ResponseText { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? ConfidenceScore { get; set; }

    public bool? IsValidated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("Airesponse")]
    public virtual ICollection<AiinteractionFeedback> AiinteractionFeedbacks { get; set; } = new List<AiinteractionFeedback>();

    [ForeignKey("StudentQuestionId")]
    [InverseProperty("Airesponses")]
    public virtual StudentQuestion StudentQuestion { get; set; } = null!;
}
