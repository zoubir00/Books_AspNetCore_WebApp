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
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HttpClient _client;
        Uri uri = new Uri(Setting.baseAddress);


        public AccountController(/*UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager*/)
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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
                    //var appuser = new IdentityUser
                    //{
                    //    Email=model.Email
                        
                        
                    //};
                    //if (registerResponse.IsSuccess)
                    //{
                    //    await _signInManager.CheckPasswordSignInAsync(appuser, model.Password, false);
                    //    
                    //}
                       
                    ViewData["success"] = "sign in is done successfully";        
                    return RedirectToAction("Index", "Books");
                    
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
    }
}
