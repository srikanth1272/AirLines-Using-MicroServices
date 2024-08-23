using ReservationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationLibrary.Repos
{
    public class EFReservationMasterRepoAsync:IReservationMasterRepoAsync
    {
        ReservationDBContext ctx = new ReservationDBContext();
       
        public async Task AddFlightSchedule(FlightSchedule flightSchedule)
        {
            try
            {
                await ctx.FlightSchedules.AddAsync(flightSchedule);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new ReservationException(ex.Message);
            }
        }

        public async Task AddReservationMaster(ReservationMaster reservationMaster)
        {
            try
            {
                await ctx.ReservationMasters.AddAsync(reservationMaster);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new ReservationException(ex.Message);
            }
        }

        public async Task<ReservationMaster> GetReservation(string PNR)
        {
            try
            {
                ReservationMaster reservationMaster = await (from r in ctx.ReservationMasters where r.PNR == PNR select r).FirstAsync();
                return reservationMaster;
            }
            catch
            {
                throw new ReservationException("No reservation master available for this PNR");
            }
        }

        public async Task<List<ReservationMaster>> GetReservationMasterByFlightNoAndDate(string fno, DateOnly date)
        {
            List<ReservationMaster> reservationMasters = await (from f in ctx.ReservationMasters where f.FlightNo == fno && f.FlightDate == date select f).ToListAsync();
            if (reservationMasters.Count > 0)
                return reservationMasters;
            else
                throw new ReservationException("No Reservation Master for this FlighNo and Date");

        }

        public async Task<List<ReservationMaster>> GetReservationMasterList()
        {
            List<ReservationMaster> reservationMasters = await ctx.ReservationMasters.ToListAsync();
            return reservationMasters;
        }

        public async Task RemoveReservationMaster(string PNR)
        {
            try
            {
                ReservationMaster reservationMaster = await GetReservation(PNR);
                ctx.Remove(reservationMaster);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);

            }

        }

        public async Task UpdateReservationMaster(string PNR, ReservationMaster reservationMaster)
        {
            try
            {
                ReservationMaster reservationMaster2 = await GetReservation(PNR);
                reservationMaster2.FlightDate = reservationMaster.FlightDate;
                reservationMaster2.FlightNo = reservationMaster.FlightNo;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);

            }
        }
    }
}
