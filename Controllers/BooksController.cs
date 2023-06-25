
using BookWebApp.Helpers;
using BookWebApp.Models;
using BookWebApp.Services;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Text;
using System.Text.Json.Serialization;

namespace BookWebApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _client;
        Uri uri = new Uri(Setting.baseAddress);

        //private readonly BookService _service;

        public BooksController(/*BookService service*/)
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
            //_service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookWithAuthorsVM> BooksList = new List<BookWithAuthorsVM>();
            HttpResponseMessage response =await _client.GetAsync(_client.BaseAddress + "/Books/Get-All-Books");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                BooksList = JsonConvert.DeserializeObject<List<BookWithAuthorsVM>>(data);
            }
            return View(BooksList);
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            BookWithAuthorsVM book = new BookWithAuthorsVM();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Books/get-book-by-id/" + id).Result;
            
                if (response.IsSuccessStatusCode)
                {
                    string data =response.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<BookWithAuthorsVM>(data);
                }
                else
                {
                    ViewBag.StatusCode = response.StatusCode;
                }
            
            return View(book);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Author> authors = new List<Author>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Authors");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                authors = JsonConvert.DeserializeObject<List<Author>>(data);
            }
            ViewBag.Authors = authors;
            return View(); 
        } 


        [HttpPost]
        public async Task<IActionResult> Create(BookVM bookVM)
        {
            try
            {
                //BookVM _book = _service.GetAuthorsIds(bookVM);
                string data = JsonConvert.SerializeObject(bookVM);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response =await _client.PostAsync(_client.BaseAddress + "/Books/Add-books-with-AuthorsandPublisher", content);
               
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessedMessage"] = "Create successed";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Create failed";
                return View(bookVM);
            }

            return View(bookVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Book book = new Book();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Books/get-book-by-id/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(data);
            }
            else
            {
                ViewBag.StatusCode = response.StatusCode;
            }

            return View(book);
           
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var response =await _client.DeleteAsync(_client.BaseAddress + "/Books/delete-book-/" + id);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
