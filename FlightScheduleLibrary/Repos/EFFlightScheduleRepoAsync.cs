using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightScheduleLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScheduleLibrary.Repos
{
    public class EFFlightScheduleRepoAsync : IFlightScheduleRepoAsync
    {
        FlightScheduleDBContext ctx = new FlightScheduleDBContext();

        public async Task AddFlight(Flight flight)
        {
            try
            {
                await ctx.Flights.AddAsync(flight);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new FlightScheduleException(ex.Message);
            }
        }

        public async Task AddFlightSchedule(FlightSchedule flightSchedule)
        {
            try
            {
                await ctx.FlightSchedules.AddAsync(flightSchedule);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new FlightScheduleException(ex.Message);
            }

        }

        public async Task<List<FlightSchedule>> GetAllFlightSchedules()
        {
            List<FlightSchedule> flights = await ctx.FlightSchedules.ToListAsync();
            return flights;

        }

        public async Task<FlightSchedule> GetFlightSchedule(string fno, DateOnly date)
        {
            try
            {
                FlightSchedule flightSchedule = await (from f in ctx.FlightSchedules where f.FlightNo == fno && f.FlightDate == date select f).FirstAsync();
                return flightSchedule;
            }
            catch
            {
                throw new FlightScheduleException("No flight schedule for this flight on this date ");
            }
        }

        public async Task<List<FlightSchedule>> GetFlightSchedulesByDate(DateOnly date)
        {
            List<FlightSchedule> flightSchedules = await (from f in ctx.FlightSchedules where f.FlightDate == date select f).ToListAsync();
            if (flightSchedules.Count > 0)
                return flightSchedules;
            else
                throw new FlightScheduleException("No Schedules on this date");
        }

        public async Task<List<FlightSchedule>> GetFlightSchedulesByFlightNo(string fno)
        {
            List<FlightSchedule> flightSchedules = await (from f in ctx.FlightSchedules where f.FlightNo == fno select f).ToListAsync();
            if (flightSchedules.Count > 0)
                return flightSchedules;
            else
                throw new FlightScheduleException("No Schedules for this flight");
        }

        public async Task RemoveFlightSchedule(string fno, DateOnly date)
        {
            try
            {

                FlightSchedule flightSchedule = await GetFlightSchedule(fno, date);
                ctx.FlightSchedules.Remove(flightSchedule);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {


                throw new FlightScheduleException(e.Message);
            }
        }

        public async Task UpdateFlightSchedule(string fno, DateOnly date, FlightSchedule flightSchedule)
        {
            try
            {
                FlightSchedule flightSchedule2 = await GetFlightSchedule(fno, date);
                flightSchedule2.DepartTime = flightSchedule.DepartTime;
                flightSchedule2.ArriveTime = flightSchedule.ArriveTime;
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new FlightScheduleException(e.Message);

            }
        }
    }
}
