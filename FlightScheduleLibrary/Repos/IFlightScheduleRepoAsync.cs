using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightScheduleLibrary.Models;


namespace FlightScheduleLibrary.Repos
{
    public interface IFlightScheduleRepoAsync
    {
        public Task AddFlightSchedule(FlightSchedule flightSchedule);
        public Task RemoveFlightSchedule(string fno, DateOnly date);
        public Task UpdateFlightSchedule(string fno, DateOnly date, FlightSchedule flightSchedule);

        public Task<FlightSchedule> GetFlightSchedule(string fno, DateOnly date);
        Task<List<FlightSchedule>> GetAllFlightSchedules();
        Task<List<FlightSchedule>> GetFlightSchedulesByFlightNo(string fno);
        Task<List<FlightSchedule>> GetFlightSchedulesByDate(DateOnly date);

        Task AddFlight(Flight flight);  
    }
}
