using ReservationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationLibrary.Repos
{
    public interface IReservationMasterRepoAsync
    {
        public Task AddReservationMaster(ReservationMaster reservationMaster);
        public Task RemoveReservationMaster(string PNR);
        public Task UpdateReservationMaster(string PNR, ReservationMaster reservationMaster);
        Task<ReservationMaster> GetReservation(string PNR);
        Task<List<ReservationMaster>> GetReservationMasterList();
        Task<List<ReservationMaster>> GetReservationMasterByFlightNoAndDate(string fno, DateOnly date);

        Task AddFlightSchedule(FlightSchedule flightSchedule);  

    }
}
