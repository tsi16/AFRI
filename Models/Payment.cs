using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class Payment
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? InstitutionId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(10)]
    public string? Currency { get; set; }

    [StringLength(50)]
    public string? Method { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaymentDate { get; set; }

    [ForeignKey("InstitutionId")]
    [InverseProperty("Payments")]
    public virtual Institution? Institution { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Payments")]
    public virtual User? User { get; set; }
}
