namespace AirLinesMVCApp.Models
{
    public class Flight
    {
        public string FlightNo { get; set; } = null!;

        public string FromCity { get; set; } = null!;

        public string ToCity { get; set; } = null!;

        public int? TotalSeats { get; set; }
    }
}
