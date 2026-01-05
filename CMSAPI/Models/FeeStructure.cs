using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class FeeStructure
{
    public int FeeStructureId { get; set; }

    public int CourseId { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Course Course { get; set; } = null!;
}
