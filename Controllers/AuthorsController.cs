using BookWebApp.Helpers;
using BookWebApp.Models;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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

        // post Add author
        public IActionResult CreateAuthor()=> View();

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorVM author)
        {
            string data = JsonConvert.SerializeObject(author);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Authors/Add-Author" , content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }   
            return View(author);
        }

        // edit author action
        public IActionResult UpdateAuthor(int Id)
        {
            AuthorVM author = new AuthorVM();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Authors/AuthorById/" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<AuthorVM>(data);
            }
            return View(author);
        }
        [HttpPost]
        public IActionResult UpdateAuthor(int id,AuthorVM author)
        {

            string data = JsonConvert.SerializeObject(author);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Authors/Edit/"+ id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

    }
}
