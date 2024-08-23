using System;
using System.Collections.Generic;

namespace FlightScheduleLibrary.Models;

public partial class FlightSchedule
{
    public string FlightNo { get; set; } = null!;

    public DateOnly FlightDate { get; set; }

    public DateTime? DepartTime { get; set; }

    public DateTime? ArriveTime { get; set; }
    public virtual Flight? FlightNoNavigation { get; set; } = null!;
}
