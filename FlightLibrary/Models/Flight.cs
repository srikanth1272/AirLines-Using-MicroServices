using System;
using System.Collections.Generic;

namespace FlightLibrary.Models;

public partial class Flight
{
    public string FlightNo { get; set; } = null!;

    public string FromCity { get; set; } = null!;

    public string ToCity { get; set; } = null!;

    public int? TotalSeats { get; set; }
}
