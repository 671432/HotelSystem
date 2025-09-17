using Microsoft.AspNetCore.Mvc;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using System.Threading.Tasks;
using ASPCustomer.Models;
using ASPCustomer.Helpers;

namespace ASPCustomer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult CustomLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomLogin(string username, string password)
        {
            // Retrieve user from database by name
            var user = await _userService.GetUserByNameAsync(username);

            // Check if user exists and password matches
            if (user != null && user.Phone == password)
            {
                // User authenticated successfully, set user object in session
                HttpContext.Session.SetObjectAsJson("CurrentUser", user);

                // Redirect to another action
                return RedirectToAction("ReservationStatus", "ReservationStatus");
            }
            else
            {
                // Authentication failed, return error message or redirect to login page
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult CustomRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomRegister(string username, string lastname, string phone)
        {
            if (ModelState.IsValid)
            {
                // Create user account using provided username and phone number
                await _userService.CreateUser(username, lastname, phone);

                // Redirect to login page after registration
                return RedirectToAction("CustomLogin");

            }

            // If we got this far, something failed, redisplay the registration form with validation errors
            return View();
        }
    }
}