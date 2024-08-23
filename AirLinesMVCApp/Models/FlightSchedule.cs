namespace AirLinesMVCApp.Models
{
    public class FlightSchedule
    {
        public string FlightNo { get; set; } = null!;

        public DateOnly FlightDate { get; set; }

        public DateTime? DepartTime { get; set; }

        public DateTime? ArriveTime { get; set; }
    }
}
