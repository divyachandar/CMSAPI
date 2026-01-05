using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class InstitutionSetting
{
    public int InstitutionId { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string InstitutionCode { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
