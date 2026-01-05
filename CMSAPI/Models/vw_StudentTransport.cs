using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class vw_StudentTransport
{
    public int AssignmentId { get; set; }

    public string StudentId { get; set; } = null!;

    public string RouteName { get; set; } = null!;

    public string PickupPoint { get; set; } = null!;

    public decimal Fee { get; set; }

    public string Status { get; set; } = null!;
}
