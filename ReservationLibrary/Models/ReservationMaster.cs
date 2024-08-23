using System;
using System.Collections.Generic;

namespace ReservationLibrary.Models;

public partial class ReservationMaster
{
    public string PNR { get; set; } = null!;

    public string? FlightNo { get; set; }

    public DateOnly? FlightDate { get; set; }

    public virtual FlightSchedule? FlightSchedule { get; set; }

    public virtual ICollection<ReservationDetail>? ReservationDetails { get; set; } = new List<ReservationDetail>();
}
