using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirLinesMVCApp.Models;
using AirLinesMVCApp.Controllers;

namespace AirLinesMVCApp.Models
{
    public class Helper
    {
        static HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5172/api/FlightSvc/") };
        static HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5172/api/FlightSchedule/") };
        static HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5172/api/ReservationMasterSvc/") };

        public static async Task<List<SelectListItem>> GetFlights()
        {

            //string token = HttpContext.Session.GetString("token");
            //client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<SelectListItem> flights = new List<SelectListItem>();
            List<FlightSchedule> flights1 = await client2.GetFromJsonAsync<List<FlightSchedule>>("");
            foreach (FlightSchedule flight in flights1)
            {
                flights.Add(new SelectListItem { Text = flight.FlightNo, Value = flight.FlightNo });
            }
            return flights;
        }
        public static async Task<List<SelectListItem>> GetFlightScheduleDates()
        {
            List<SelectListItem> flightDates = new List<SelectListItem>();
            List<FlightSchedule> flights = await client2.GetFromJsonAsync<List<FlightSchedule>>("FlightSchedule");
            foreach (FlightSchedule flight in flights)
            {
                flightDates.Add(new SelectListItem { Text = flight.FlightDate.ToString("MM-dd-yyyy"), Value = flight.FlightDate.ToString("MM-dd-yyyy") });
            }
            return flightDates;
        }
        public static async Task<List<SelectListItem>> GetFlightScheduleNo()
        {
            List<SelectListItem> flightNos = new List<SelectListItem>();
            List<FlightSchedule> flights = await client2.GetFromJsonAsync<List<FlightSchedule>>("FlightSchedule");
            foreach (FlightSchedule flight in flights)
            {
                flightNos.Add(new SelectListItem { Text = flight.FlightNo, Value = flight.FlightNo });
            }
            return flightNos;
        }
        public static async Task<List<SelectListItem>> GetPNRs()
        {
            List<SelectListItem> pnrs = new List<SelectListItem>();
            List<ReservationMaster> reservationMasters = await client3.GetFromJsonAsync<List<ReservationMaster>>("ReservationMaster");
            foreach (ReservationMaster reservationMaster in reservationMasters)
            {
                pnrs.Add(new SelectListItem { Text = reservationMaster.PNR, Value = reservationMaster.PNR });
            }
            return pnrs;
        }
    }
}
