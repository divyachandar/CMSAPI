using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class NotificationSetting
{
    public int NotificationSettingId { get; set; }

    public bool EnableNotifications { get; set; }

    public bool EnableEmailNotifications { get; set; }

    public bool EnableSmsnotifications { get; set; }

    public DateTime UpdatedAt { get; set; }
}
