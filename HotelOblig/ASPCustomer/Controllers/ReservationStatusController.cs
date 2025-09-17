using ASPCustomer.Helpers;
using ASPCustomer.Models;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPCustomer.Controllers
{
    public class ReservationStatusController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationStatusController(IReservationService reservationService, IHttpContextAccessor httpContextAccessor)
        {
            _reservationService = reservationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ReservationStatus()
        {
            // Get the current user's ID
            int userId = GetCurrentUserId();

            // Retrieve reservations for the current user
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);

            // Pass reservations to the view
            return View(reservations);
        }

        private int GetCurrentUserId()
        {
            // Retrieving the user object from the session
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("CurrentUser");
            // Retrieving the UserId from the retrieved user object
            var userId = currentUser?.UserId;

            // If userId is null, handle it as per your requirement
            // For example, redirect to login page
            if (userId == null)
            {
                return -1; // Or any other handling logic
            }

            return userId.Value;
        }
    }
}