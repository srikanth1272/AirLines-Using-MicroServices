using System;
using System.Collections.Generic;

namespace FlightScheduleLibrary.Models;

public partial class Flight
{
    public string FlightNo { get; set; } = null!;

    public virtual ICollection<FlightSchedule>? FlightSchedules { get; set; } = new List<FlightSchedule>();
}
