using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afri.Models;

public partial class RolePermission
{
    [Key]
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("RolePermissions")]
    public virtual Permission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("RolePermissions")]
    public virtual Role Role { get; set; } = null!;
}
