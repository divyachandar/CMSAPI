using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class StaffProfile
{
    public int StaffProfileId { get; set; }

    public int UserId { get; set; }

    public string? Designation { get; set; }

    public virtual User User { get; set; } = null!;
}
