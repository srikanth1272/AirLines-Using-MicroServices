using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AirLinesMVCApp.Models;


namespace AirLinesMVCApp.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5172/flightSvc/") };
       
        public async Task<ActionResult> Index()
        {
             string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Flight> flights = await client.GetFromJsonAsync<List<Flight>>("");
            return View(flights);
        }
        public async Task<ActionResult> Details(string fno)
        {
            Flight flight = await client.GetFromJsonAsync<Flight>(""+fno);
            return View(flight);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create(Flight flight)
        {

            
            await client.PostAsJsonAsync("", flight);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Edit(string  fno)
        {
            Flight flight = await client.GetFromJsonAsync<Flight>("" + fno);
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Flight flight)
        {
                string fno = flight.FlightNo;  
                await client.PutAsJsonAsync("" + fno, flight);
                return RedirectToAction(nameof(Index));
        }
        [Route("Flight/Delete/{fno}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Delete(string fno)
        {
            Flight flight = await client.GetFromJsonAsync<Flight>("" + fno);
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Flight/Delete/{fno}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string fno,IFormCollection collection)
        {

            //string fno = flight.FlightNo;
            await client.DeleteAsync("" + fno);
            return RedirectToAction(nameof(Index));
        }
    }
}
