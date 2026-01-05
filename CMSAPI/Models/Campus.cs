using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class Campus
{
    public int CampusId { get; set; }

    public string CampusName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
