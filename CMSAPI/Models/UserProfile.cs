using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class UserProfile
{
    public int ProfileId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? DepartmentId { get; set; }

    public string? AvatarUrl { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Department? Department { get; set; }

    public virtual User User { get; set; } = null!;
}
