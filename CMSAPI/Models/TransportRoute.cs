using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class TransportRoute
{
    public int RouteId { get; set; }

    public string RouteName { get; set; } = null!;

    public string VehicleNumber { get; set; } = null!;

    public string DriverName { get; set; } = null!;

    public string DriverPhone { get; set; } = null!;

    public int Capacity { get; set; }

    public int CurrentStudents { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<StudentTransportAssignment> StudentTransportAssignments { get; set; } = new List<StudentTransportAssignment>();

    public virtual ICollection<TransportRouteStop> TransportRouteStops { get; set; } = new List<TransportRouteStop>();
}
