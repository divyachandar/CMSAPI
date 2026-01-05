using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class AdmissionApplication
{
    public Guid ApplicationId { get; set; }

    public string ApplicationNumber { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int CourseId { get; set; }

    public int CampusId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly AppliedDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AdmissionDocument> AdmissionDocuments { get; set; } = new List<AdmissionDocument>();

    public virtual ICollection<AdmissionStatusHistory> AdmissionStatusHistories { get; set; } = new List<AdmissionStatusHistory>();

    public virtual Campus Campus { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
