using System;
using System.Collections.Generic;

namespace ReservationLibrary.Models;

public partial class FlightSchedule
{
    public string FlightNo { get; set; } = null!;

    public DateOnly FlightDate { get; set; }

    public virtual ICollection<ReservationMaster>? ReservationMasters { get; set; } = new List<ReservationMaster>();
}
