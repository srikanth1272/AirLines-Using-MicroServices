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
    public class ReservationDetailController : ControllerBase
    {
        IReservationDetailRepoAsync repo;
        public ReservationDetailController(IReservationDetailRepoAsync repository)
        {
            repo = repository;
        }
        [HttpGet("{pnr}")]
        public async Task<ActionResult> GetByPNR(string pnr)
        {
            try
            {
                List<ReservationDetail> reservationDetail = await repo.GetReservationDetailsByPNR(pnr);
                return Ok(reservationDetail);
            }
            catch (ReservationException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("{pnr}/{pno}")]
        public async Task<ActionResult> GetOne(string pnr, int pno)
        {
            try
            {
                ReservationDetail reservationDetail = await repo.GetReservationDetail(pnr, pno);
                return Ok(reservationDetail);
            }
            catch (ReservationException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(ReservationDetail reservationDetail)
        {
            try
            {
                await repo.AddReservationDetail(reservationDetail);
                return Created($"api/ReservationDetail/{reservationDetail.PNR}/{reservationDetail.PassengerNo}", reservationDetail);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{pnr}/{pno}")]
        public async Task<ActionResult> Update(string pnr, int pno, ReservationDetail reservationDetail)
        {
            try
            {
                await repo.UpdateReservationDetail(pnr, pno, reservationDetail);
                return Ok(reservationDetail);
            }
            catch (ReservationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{pnr}/{pno}")]
        public async Task<ActionResult> Delete(string pnr, int pno)
        {
            try
            {
                await repo.RemoveReservationDetail(pnr, pno);
                return Ok();
            }
            catch (ReservationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
