using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationLibrary.Models;
using ReservationLibrary.Repos;

namespace ReservationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationMasterController : ControllerBase
    {
        IReservationMasterRepoAsync repo;
        public ReservationMasterController(IReservationMasterRepoAsync repository)
        {
            repo = repository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<ReservationMaster> reservationMasters = await repo.GetReservationMasterList();
            return Ok(reservationMasters);
        }
        [HttpGet("{fno}/{date}")]
        public async Task<ActionResult> GetByFnoAndDate(string fno, DateOnly date)
        {
            try
            {
                List<ReservationMaster> reservationMaster = await repo.GetReservationMasterByFlightNoAndDate(fno, date);
                return Ok(reservationMaster);
            }
            catch (ReservationException ex) {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{pnr}")]
        public async Task<ActionResult> GetOne(string pnr)
        {
            try
            {
                ReservationMaster reservationMaster = await repo.GetReservation(pnr);
                return Ok(reservationMaster);
            }
            catch (ReservationException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(ReservationMaster reservationMaster)
        {
            try
            {
                await repo.AddReservationMaster(reservationMaster);
                return Created($"api/ReservationMaster/{reservationMaster.PNR}", reservationMaster);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("FlightSchedule")]
        public async Task<ActionResult> InsertFlightSchedule(FlightSchedule flightSchedule)
        {
            try
            {
                await repo.AddFlightSchedule(flightSchedule);
                return Created();
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{pnr}")]
        public async Task<ActionResult> Update(string pnr, ReservationMaster reservationMaster)
        {
            try
            {
                await repo.UpdateReservationMaster(pnr, reservationMaster);
                return Ok(reservationMaster);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{pnr}")]
        public async Task<ActionResult> Delete(string pnr)
        {
            try
            {
                await repo.RemoveReservationMaster(pnr);
                return Ok();
            }
            catch (ReservationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
