using BookWebApp.Helpers;
using BookWebApp.Models;
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

        public BooksController()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
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
       
        
                   
        //[HttpPost]
        //public async Task<IActionResult> Details()
        //{
        //    Book book = new Book();
        //    using (var response = await _client.GetAsync(_client.BaseAddress+"/Books/get-book-by-id/"))
        //    {
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            book = JsonConvert.DeserializeObject<Book>(apiResponse);
        //        }
        //        else
        //        {
        //            ViewBag.StatusCode = response.StatusCode;
        //        }
        //    }
           
        //    return View(book);
        //}

        //[HttpGet]
        //public IActionResult AddBook() => View();
        

        //[HttpPost]
        //public async Task<IActionResult> AddBook(BookVM bookVM)
        //{
        //    try
        //    {
        //        List<int> rr = new List<int>();
        //        bookVM.AuthorsIds = rr;
        //        string data = JsonConvert.SerializeObject(bookVM);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Books/Add-books-with-AuthorsandPublisher", content).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessedMessage"] = "Create successed";
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        TempData["ErrorMessage"] = "Create failed";
        //        return View(bookVM);
        //    }
           
        //    return View(bookVM);
        //}

    }
}
