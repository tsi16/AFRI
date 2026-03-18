using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class SubscriptionPlan
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    public int? DurationDays { get; set; }

    public string? FeatureFlags { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [InverseProperty("SubscriptionPlan")]
    public virtual ICollection<InstitutionSubscription> InstitutionSubscriptions { get; set; } = new List<InstitutionSubscription>();

    [InverseProperty("SubscriptionPlan")]
    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
