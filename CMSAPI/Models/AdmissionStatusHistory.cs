using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class AdmissionStatusHistory
{
    public int HistoryId { get; set; }

    public Guid ApplicationId { get; set; }

    public string OldStatus { get; set; } = null!;

    public string NewStatus { get; set; } = null!;

    public DateTime ChangedAt { get; set; }

    public string? ChangedBy { get; set; }

    public virtual AdmissionApplication Application { get; set; } = null!;
}
