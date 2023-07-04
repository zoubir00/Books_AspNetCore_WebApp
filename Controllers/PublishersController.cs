using BookWebApp.Helpers;
using BookWebApp.Models;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookWebApp.Controllers
{
    public class PublishersController : Controller
    {
        private readonly HttpClient _client;
        Uri uri = new Uri(Setting.baseAddress);
        public PublishersController()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
        }
        public IActionResult Index()
        {
            List<PublisherwithBooksAndAuthorVM> publishers = new List<PublisherwithBooksAndAuthorVM>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Publishers/getPublishres").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                publishers = JsonConvert.DeserializeObject<List<PublisherwithBooksAndAuthorVM>>(data);
            }
            return View(publishers);
        }

        public async Task<IActionResult> Details(int Id)
        {
            PublisherwithBooksAndAuthorVM Publisher = new PublisherwithBooksAndAuthorVM();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Publishers/GetPublisher-Data/" + Id);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Publisher = JsonConvert.DeserializeObject<PublisherwithBooksAndAuthorVM>(data);
            }
           
           
            return View(Publisher);
        }

        // Create method
        public IActionResult CreatePublisher() => View();

        [HttpPost]
        public IActionResult CreatePublisher(PublisherVM publisher) 
        {
            string data = JsonConvert.SerializeObject(publisher);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Publishers/Add-publisher", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(publisher); 
        }

        // delete publisher
        public async Task<IActionResult> Delete(int id)
        {
            PublisherwithBooksAndAuthorVM publisher = new PublisherwithBooksAndAuthorVM();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Publishers/GetPublisher-Data/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                publisher = JsonConvert.DeserializeObject<PublisherwithBooksAndAuthorVM>(data);
            }
            else
            {
                ViewBag.StatusCode = response.StatusCode;
            }

            return View(publisher);

        }

        //Post : delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var response = await _client.DeleteAsync(_client.BaseAddress + "/Publishers/Delete-Publisher-Data/" + id);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
