using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AirLinesMVCApp.Models;


namespace AirLinesMVCApp.Controllers
{
    [Authorize]
    public class FlightScheduleController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5171/FlightScheduleSvc/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<FlightSchedule> flightSchedules = await client.GetFromJsonAsync<List<FlightSchedule>>("");
            return View(flightSchedules);
        }
        public async Task<ActionResult> FlightSchedulesByFlightNo(string fno)
        {
            List<FlightSchedule> flightSchedules = await client.GetFromJsonAsync<List<FlightSchedule>>("ByFno/"+fno);
            return View(flightSchedules);
        }
        public async Task<ActionResult> FlightSchedulesByDate(DateOnly date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            List<FlightSchedule> flightSchedules = await client.GetFromJsonAsync<List<FlightSchedule>>("ByDate/"+formattedDate);
            return View(flightSchedules);
        }


        public async Task<ActionResult> Details(string fno, DateOnly date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            FlightSchedule flightSchedule = await client.GetFromJsonAsync<FlightSchedule>(""+fno+"/"+formattedDate);
            return View(flightSchedule);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FlightSchedule flightSchedule)
        {
            try
            {
                await client.PostAsJsonAsync("", flightSchedule);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        public async Task<ActionResult> Edit(string fno, DateOnly date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            FlightSchedule flightSchedule = await client.GetFromJsonAsync<FlightSchedule>("" + fno + "/" + formattedDate);
            return View(flightSchedule);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FlightSchedule flightSchedule)
        {
            try
            {
                string fno = flightSchedule.FlightNo;
                string formattedDate = flightSchedule.FlightDate.ToString("yyyy-MM-dd");
                await client.PutAsJsonAsync("" + fno + "/" + formattedDate, flightSchedule);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string fno, DateOnly date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            FlightSchedule flightSchedule = await client.GetFromJsonAsync<FlightSchedule>("" + fno + "/" + formattedDate);
            return View(flightSchedule);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(FlightSchedule flightSchedule)
        {
            try
            {
                string fno = flightSchedule.FlightNo;
                string formattedDate = flightSchedule.FlightDate.ToString("yyyy-MM-dd");
                await client.DeleteAsync(""+fno  + "/" + formattedDate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
