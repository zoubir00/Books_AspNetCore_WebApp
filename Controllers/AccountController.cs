using BookWebApp.Helpers;
using BookWebApp.Models;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace BookWebApp.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HttpClient _client;
        Uri uri = new Uri(Setting.baseAddress);


        public AccountController(/*UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager*/)
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }


        // registration
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        // registration
        [HttpPost]
        public async Task<ActionResult<UserManagerResponse>> Register(RegisterVM model)
        {
            try
            {

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Accounts/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    var resposeContent = await response.Content.ReadAsStringAsync();
                    var registerResponse = JsonConvert.DeserializeObject<UserManagerResponse>(resposeContent);
                    if (registerResponse.IsSuccess)
                    {
                        //await _signInManager.SignInAsync(registerResponse.User, false);
                        ViewData["success"] = "sign in is done successfully";
                            return RedirectToAction("Index", "Books");
                    }

                   

                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Create failed";
                return View(model);
            }

            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }

        // login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserManagerResponse>> Login(LoginVM model)
        {
            try
            {

                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Accounts/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var resposeContent = await response.Content.ReadAsStringAsync();
                    var registerResponse = JsonConvert.DeserializeObject<UserManagerResponse>(resposeContent);
                    if (registerResponse.IsSuccess)
                    {
                        //await _userManager.CheckPasswordAsync(registerResponse.User, model.Password);
                        //await _signInManager.SignInAsync(registerResponse.User, false);
                        ViewData["success"] = "sign in is done successfully";
                         return RedirectToAction("Index", "Books");
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Create failed";
                return View(model);
            }

            return View(model);
        }

    }


}
