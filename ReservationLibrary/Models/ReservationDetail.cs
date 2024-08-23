using System;
using System.Collections.Generic;

namespace ReservationLibrary.Models;

public partial class ReservationDetail
{
    public string PNR { get; set; } = null!;

    public int PassengerNo { get; set; }

    public string PassengerName { get; set; } = null!;

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public virtual ReservationMaster? PNRNavigation { get; set; } = null!;
}
