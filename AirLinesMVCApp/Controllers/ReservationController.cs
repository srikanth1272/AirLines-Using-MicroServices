using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AirLinesMVCApp.Models;
namespace AirLinesMVCApp.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        static HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5172/ReservationMasterSvc/") };
        static HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5172/passengerSvc/") };


        public async  Task<ActionResult> ReservationMaster()
        {
            string token = HttpContext.Session.GetString("token");
            client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<ReservationMaster> reservationMasters = await client1.GetFromJsonAsync<List<ReservationMaster>>("");
            
            return View(reservationMasters);
        }
        public async Task<ActionResult> RMByFlightNoAndDate(string fno,DateOnly date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            List<ReservationMaster> reservationMasters = await client1.GetFromJsonAsync<List<ReservationMaster>>(""+fno+"/"+formattedDate);
            return View(reservationMasters);
        }
        public async Task<ActionResult> ReservationDetails(string pnr)
        { 
           string token = HttpContext.Session.GetString("token");
           client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<ReservationDetail> reservationDetails = await client2.GetFromJsonAsync<List<ReservationDetail>>(""+pnr);
            ViewData["PNR"] = pnr;
            pno = reservationDetails.Count();
            return View(reservationDetails);
        }

      
        // GET: ReservationController/Details/5
        public async Task<ActionResult> RMDetails(string pnr)
        {
            ReservationMaster reservationMaster = await client1.GetFromJsonAsync<ReservationMaster>(""+pnr);
            return View(reservationMaster);
        }
        public async Task<ActionResult> RDDetails(string pnr, int pno)
        {
           ReservationDetail reservationDetail = await client2.GetFromJsonAsync<ReservationDetail>(""+pnr+"/"+pno);
            return View(reservationDetail);
        }

        // GET: ReservationController/Create
        public async Task<ActionResult> RMCreate()
        {
            return View();
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RMCreate(ReservationMaster reservationMaster)
        {
                 await client1.PostAsJsonAsync("",reservationMaster);
              
                return RedirectToAction(nameof(ReservationMaster));
        }
        static int pno;
        public ActionResult RDCreate(string pnr)
        {
            ReservationDetail reservationDetail = new ReservationDetail();
            reservationDetail.PNR = pnr;
            reservationDetail.PassengerNo = pno+1;
            return View(reservationDetail);
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> RDCreate(ReservationDetail reservationDetail)
        {
            try
            {
                await client2.PostAsJsonAsync("", reservationDetail);
                return RedirectToAction(nameof(ReservationDetails),new { pnr = reservationDetail.PNR });
            }
            catch
            {
                return View();
            }
        }
        // GET: ReservationController/Edit/5
        public async Task<ActionResult> RMEdit(string pnr)
        {
            ReservationMaster reservation = await client1.GetFromJsonAsync<ReservationMaster>("" + pnr);
            return View(reservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RMEdit(string pnr, ReservationMaster reservationMaster)
        {
            try
            {
                await client1.PutAsJsonAsync(""+pnr, reservationMaster);
                return RedirectToAction(nameof(ReservationMaster));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RMDelete(string pnr)
        {
            ReservationMaster reservation = await client1.GetFromJsonAsync<ReservationMaster>("" + pnr);
            return View(reservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RMDelete(string pnr, ReservationMaster reservationMaster)
        {
            try
            {
                await client1.DeleteAsync(""+pnr);
                return RedirectToAction(nameof(ReservationMaster));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> RDEdit(string pnr,int pno)
        {
            ReservationDetail reservation = await client2.GetFromJsonAsync<ReservationDetail>("" + pnr + "/" + pno);
            return View(reservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RDEdit(ReservationDetail reservationDetail)
        {
            try
            {
                await client2.PutAsJsonAsync(""+reservationDetail.PNR+"/"+reservationDetail.PassengerNo, reservationDetail);
                return RedirectToAction(nameof(ReservationDetails), new { pnr = reservationDetail.PNR });
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationController/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RDDelete(string pnr, int pno)
        {
            ReservationDetail reservation = await client2.GetFromJsonAsync<ReservationDetail>("" + pnr + "/" + pno);

            return View(reservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RDDelete(ReservationDetail reservationDetail)
        {
            try
            {
                await client2.DeleteAsync("" + reservationDetail.PNR + "/" + reservationDetail.PassengerNo);
                return RedirectToAction(nameof(ReservationDetails), new { pnr = reservationDetail.PNR });
            }
            catch
            {
                return View();
            }
        }
    }
}
