using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class VwStudentDistribution
{
    public string CourseName { get; set; } = null!;

    public int? StudentCount { get; set; }
}
