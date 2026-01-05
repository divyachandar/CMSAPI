using System;
using System.Collections.Generic;

namespace CMSAPI.Models;

public partial class TransportRouteStop
{
    public int StopId { get; set; }

    public int RouteId { get; set; }

    public string StopName { get; set; } = null!;

    public int StopOrder { get; set; }

    public virtual TransportRoute Route { get; set; } = null!;
}
