using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class StudentTransportAssignment
{
    public int AssignmentId { get; set; }

    public string StudentId { get; set; } = null!;

    public int RouteId { get; set; }

    public string PickupPoint { get; set; } = null!;

    public decimal Fee { get; set; }

    public string Status { get; set; } = null!;

    public DateTime AssignedAt { get; set; }

    public virtual TransportRoute Route { get; set; } = null!;
}
