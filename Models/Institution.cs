using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class Institution
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(100)]
    public string? Region { get; set; }

    [StringLength(150)]
    public string? ContactEmail { get; set; }

    [StringLength(50)]
    public string? ContactPhone { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("Institution")]
    public virtual ICollection<InstitutionSubscription> InstitutionSubscriptions { get; set; } = new List<InstitutionSubscription>();

    [InverseProperty("Institution")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [InverseProperty("Institution")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
