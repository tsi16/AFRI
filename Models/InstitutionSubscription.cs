using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class InstitutionSubscription
{
    [Key]
    public int Id { get; set; }

    public int InstitutionId { get; set; }

    public int SubscriptionPlanId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("InstitutionId")]
    [InverseProperty("InstitutionSubscriptions")]
    public virtual Institution Institution { get; set; } = null!;

    [ForeignKey("SubscriptionPlanId")]
    [InverseProperty("InstitutionSubscriptions")]
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;
}
