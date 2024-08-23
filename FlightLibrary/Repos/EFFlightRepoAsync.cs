using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightLibrary.Models;
using Microsoft.EntityFrameworkCore;


namespace FlightLibrary.Repos
{
    public class EFFlightRepoAsync : IFlightRepoAsync
    {
       FlightDBContext ctx = new FlightDBContext();

        public async Task AddFlightDetails(Flight flight)
        {
            try
            {
                await ctx.AddAsync(flight);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new FlightException(ex.Message);
            }
        }

        public async Task<List<Flight>> GetAllFlightDetails()
        {

            List<Flight> flights = await ctx.Flights.ToListAsync();
            return flights;

        }

        public async Task<Flight> GetFlight(string fno)
        {
            try
            {
                Flight flight = await (from f in ctx.Flights where f.FlightNo == fno select f).FirstAsync();
                return flight;
            }
            catch
            {
                throw new FlightException("No Flight found");
            }
        }

        public async Task RemoveFlightDetails(string fno)
        {
            try
            {
                Flight flight = await GetFlight(fno);
                ctx.Flights.Remove(flight);
                await ctx.SaveChangesAsync();
            }
            catch (FlightException e)
            {
                throw new FlightException(e.Message);
            }
        }

        public async Task UpdateFlightDetails(string fNo, Flight flight)
        {
            try
            {
                Flight flight2 = await GetFlight(fNo);
                flight2.FromCity = flight.FromCity;
                flight2.ToCity = flight.ToCity;
                flight2.TotalSeats = flight.TotalSeats;
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new FlightException(e.Message);
            }
        }
    }
}
