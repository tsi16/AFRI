using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class UserSubscription
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SubscriptionPlanId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("SubscriptionPlanId")]
    [InverseProperty("UserSubscriptions")]
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserSubscriptions")]
    public virtual User User { get; set; } = null!;
}
