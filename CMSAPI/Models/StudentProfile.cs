using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class StudentProfile
{
    public int StudentProfileId { get; set; }

    public int UserId { get; set; }

    public string? RollNumber { get; set; }

    public int? YearOfStudy { get; set; }

    public virtual User User { get; set; } = null!;
}
