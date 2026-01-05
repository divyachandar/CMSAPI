using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class AdmissionDocument
{
    public int DocumentId { get; set; }

    public Guid ApplicationId { get; set; }

    public string FileName { get; set; } = null!;

    public string? FilePath { get; set; }

    public DateTime UploadedAt { get; set; }

    public virtual AdmissionApplication Application { get; set; } = null!;
}
