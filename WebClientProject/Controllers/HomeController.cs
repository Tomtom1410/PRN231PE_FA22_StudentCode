using BusinessObjects.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text.Json;
using WebClientProject.Models;

namespace WebClientProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpClient = null;
        private const string profileUrl = "https://localhost:7067/api/PropertyProfile/";
        private const string rentUrl = "https://localhost:7067/api/Renting/GetAllRenting";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(HRStaffDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(profileUrl + "Login", model);
            if (response.IsSuccessStatusCode)
            {
                var account = await response.Content.ReadFromJsonAsync<HRStaffDto>();
                HttpContext.Session.SetString("account", JsonSerializer.Serialize(account));
                return Redirect("List");
            }

            ViewData["msg"] = "Email or password is invalid!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> List(string? search)
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }

            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }
            ViewBag.SearchValue = search;
            HttpResponseMessage response = null;

            if (string.IsNullOrEmpty(search))
            {
                 response = await _httpClient.GetAsync(profileUrl + $"GetAllPropertyProfile");
            }
            else
            {
                response = await _httpClient.GetAsync(profileUrl + $"GetAllPropertyProfile?$filter=contains(Name, '{search}') or contains(Location, '{search}')");
            }

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<PropertyProfileDto>>();
                return View(data);
            }

            ViewData["msg"] = "Have an error in processing!";
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Renting(string? search)
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }

            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }

            HttpResponseMessage response = null;
            ViewBag.SearchValue = search;
            if (string.IsNullOrEmpty(search))
            {
                response = await _httpClient.GetAsync(rentUrl);
            }
            else
            {
                response = await _httpClient.GetAsync(rentUrl + $"?$filter=Company/CompanyName eq '{search}'");
            }

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<RentingDto>>();
                return View(data);
            }

            ViewData["msg"] = "Have an error in processing!";
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }

            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(PropertyProfileDto model)
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }
            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(profileUrl + "Create", model);

            if (response.IsSuccessStatusCode)
            {
                ViewData["msg"] = "Create Success!";
                return View();
            }
            ViewData["msg"] = "Have an error in processing!";
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }

            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }
            HttpResponseMessage response = await _httpClient.GetAsync(profileUrl + $"GetAllPropertyProfile?$filter=propertyProfileID eq {id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<PropertyProfileDto>>();
                if (data.Count() == 0)
                {
                    return NotFound();
                }

                return View(data[0]);
            }
            ViewData["msg"] = "Have an error in processing!";
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PropertyProfileDto model)
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }
            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(profileUrl + "Update", model);

            if (response.IsSuccessStatusCode)
            {
                ViewData["msg"] = "Update Success!";
                return View();
            }
            ViewData["msg"] = "Have an error in processing!";
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var accountValue = HttpContext.Session.GetString("account");
            if (accountValue == null)
            {
                return Redirect("Login");
            }
            var account = JsonSerializer.Deserialize<HRStaffDto>(accountValue);
            if (account.Role != Role.Administrator)
            {
                ViewData["msg"] = "You are not allowed to access this function!";
                return View("Error");
            }

            HttpResponseMessage responseDelete = await _httpClient.DeleteAsync(profileUrl + $"Delete/{id}");
            if (responseDelete.IsSuccessStatusCode)
            {
                return Redirect("../List");
            }
            ViewData["msg"] = "Have an error in processing!";
            return View("Error");
        }
    }
}