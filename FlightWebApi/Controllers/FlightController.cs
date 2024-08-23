using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightLibrary.Models;
using FlightLibrary.Repos;
using Microsoft.AspNetCore.Authorization;

namespace FlightWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightController : ControllerBase
    {
        IFlightRepoAsync repo;
        public FlightController(IFlightRepoAsync repository)
        {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Flight> flights = await repo.GetAllFlightDetails();
            return Ok(flights);
        }
        [HttpGet("{fno}")]
        public async Task<ActionResult> GetOne(string fno)
        {
            try
            {
                Flight flight = await repo.GetFlight(fno);
                return Ok(flight);
            }
            catch (FlightException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Flight flight)

        {
            try
            {
                await repo.AddFlightDetails(flight);
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5115/api/FlightSchedule/") };
                await client.PostAsJsonAsync("Flight/",new { FlightNo = flight.FlightNo });
                return Created($"api/Flight/{flight.FlightNo}", flight);
            }
            catch (FlightException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{fno}")]
        public async Task<ActionResult> Update(string fno, Flight flight)
        {
            try
            {
                await repo.UpdateFlightDetails(fno, flight);
                return Ok(flight);
            }
            catch (FlightException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{fno}")]
        public async Task<ActionResult> Delete(string fno)
        {
            try
            {
                await repo.RemoveFlightDetails(fno);
                return Ok();
            }
            catch (FlightException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
