namespace AirLinesMVCApp.Models
{
    public class ReservationMaster
    {
        public string PNR { get; set; } = null!;

        public string? FlightNo { get; set; }

        public DateOnly? FlightDate { get; set; }
    }
}
