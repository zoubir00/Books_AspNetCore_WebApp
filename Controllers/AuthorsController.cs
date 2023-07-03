using BookWebApp.Helpers;
using BookWebApp.Models;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookWebApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient _client;
        Uri uri = new Uri(Setting.baseAddress);

       

        public AuthorsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
            
        }
        //List authors
        public async Task<IActionResult> Index()
        {
            List<Author> AuthorList = new List<Author>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Authors");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                AuthorList = JsonConvert.DeserializeObject<List<Author>>(data);
            }
            return View(AuthorList);
           
        }

        // get author by Id
        public async Task<IActionResult> GetAuthorById(int Id)
        {
            AuthorwithBooksVM author = new AuthorwithBooksVM();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Authors/Get-Authors-with-Books/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<AuthorwithBooksVM>(data);
            }
            return View(author);
        }
    }
}
