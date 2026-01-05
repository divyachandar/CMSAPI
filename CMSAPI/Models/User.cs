using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual StaffProfile? StaffProfile { get; set; }

    public virtual StudentProfile? StudentProfile { get; set; }

    public virtual UserProfile? UserProfile { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
