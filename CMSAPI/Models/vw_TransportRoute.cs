using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class vw_TransportRoute
{
    public int RouteId { get; set; }

    public string RouteName { get; set; } = null!;

    public string VehicleNumber { get; set; } = null!;

    public string DriverName { get; set; } = null!;

    public string DriverPhone { get; set; } = null!;

    public int Capacity { get; set; }

    public int CurrentStudents { get; set; }

    public string Status { get; set; } = null!;
}
