using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class SecuritySetting
{
    public int SecuritySettingId { get; set; }

    public bool EnableTwoFactorAuth { get; set; }

    public bool RequireStrongPasswords { get; set; }

    public bool EnableSessionTimeout { get; set; }

    public int SessionTimeoutMinutes { get; set; }

    public DateTime UpdatedAt { get; set; }
}
