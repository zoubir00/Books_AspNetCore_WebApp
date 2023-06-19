using BookWebApp.Models;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BookWebApp.Controllers
{
    public class BooksController : Controller
    {
        Uri baseAddress = new Uri("https://book-web-api.azurewebsites.net/api/");
        private readonly HttpClient _client;
        public BooksController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookVM> BooksList = new List<BookVM>();
            HttpResponseMessage response =await _client.GetAsync(_client.BaseAddress + "/Books/Get-All-Books");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                BooksList = JsonConvert.DeserializeObject<List<BookVM>>(data);
            }
            return View(BooksList);
        }
    }
}
