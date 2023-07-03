using BookWebApp.Helpers;
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
        public IActionResult CreatePublisher() => View();

        [HttpPost]
        public IActionResult CreatePublisher(PublisherVM publisher) 
        {
            string data = JsonConvert.SerializeObject(publisher);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Publishers/Add-publisher", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Books");
            }
            return View(publisher); 
        }
    }
}
