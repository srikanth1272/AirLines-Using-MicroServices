using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightScheduleLibrary.Models;
using FlightScheduleLibrary.Repos;
using Microsoft.AspNetCore.Authorization;

namespace FlightScheduleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightScheduleController : ControllerBase
    {
        IFlightScheduleRepoAsync repo;

        public FlightScheduleController(IFlightScheduleRepoAsync repository)
        {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<FlightSchedule> flights = await repo.GetAllFlightSchedules();
            return Ok(flights);
        }
        [HttpGet("ByFno/{fno}")]
        public async Task<ActionResult> GetByfno(string fno)
        {
            try
            {
                List<FlightSchedule> flights = await repo.GetFlightSchedulesByFlightNo(fno);
                return Ok(flights);
            }
            catch (FlightScheduleException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByDate/{date}")]
        public async Task<ActionResult> GetByDate(DateOnly date)
        {
            try
            {
                List<FlightSchedule> flights = await repo.GetFlightSchedulesByDate(date);
                return Ok(flights);
            }
            catch (FlightScheduleException ex) { 
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{fno}/{date}")]
        public async Task<ActionResult> GetOne(string fno, DateOnly date)
        {
            try
            {
                DateOnly formattedDate = date;
                FlightSchedule flight = await repo.GetFlightSchedule(fno, formattedDate);
                return Ok(flight);
            }
            catch (FlightScheduleException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(FlightSchedule flightSchedule)
        {
            try
            {
                await repo.AddFlightSchedule(flightSchedule);
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5135/api/ReservationMaster/") };
                await client.PostAsJsonAsync("FlightSchedule/", new {   FlightNo= flightSchedule.FlightNo , FlightDate = flightSchedule.FlightDate});
                return Created($"api/FlightSchedule/{flightSchedule.FlightNo}/{flightSchedule.FlightDate}", flightSchedule);
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Flight")]
        public async Task<ActionResult> AddFlight(Flight flight)
        {
            try
            {
                await repo.AddFlight(flight);
                return Created();
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{fno}/{date}")]
        public async Task<ActionResult> Update(string fno, DateOnly date, FlightSchedule flightSchedule)
        {
            try
            {
                DateOnly formattedDate = date;
                await repo.UpdateFlightSchedule(fno, formattedDate, flightSchedule);
                return Ok(flightSchedule);
            }
            catch (FlightScheduleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{fno}/{date}")]
        public async Task<ActionResult> Delete(string fno, DateOnly date)
        {
            try
            {
                await repo.RemoveFlightSchedule(fno, date);
                return Ok();
            }
            catch (FlightScheduleException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
