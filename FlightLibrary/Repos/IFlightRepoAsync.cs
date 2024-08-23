using FlightLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLibrary.Repos
{
    public interface IFlightRepoAsync
    {
        public Task AddFlightDetails(Flight flight);
        public Task RemoveFlightDetails(string fno);
        public Task UpdateFlightDetails(string fNo, Flight flight);

        Task<Flight> GetFlight(string fno);
        Task<List<Flight>> GetAllFlightDetails();
    }
}
