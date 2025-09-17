using Microsoft.AspNetCore.Mvc;
using HotelLibrary.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using ASPCustomer.Helpers;
using HotelLibrary.Models;

namespace ASPCustomer.Controllers
{
    public class BookRoomController : Controller
    {
        private readonly IReservationService _reservationService;

        public BookRoomController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult BookRoom(int roomId)
        {
            ViewBag.RoomId = roomId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(DateOnly fromDate, DateOnly toDate, int roomId)
        {
            // Check if user is logged in
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("CurrentUser");
            // Retrieving the UserId from the retrieved user object
            var userId = currentUser?.UserId;
            if (!userId.HasValue)
            {
                // User is not logged in, redirect to login page
                return RedirectToAction("CustomLogin", "Account");
            }

            // Perform booking logic
            await _reservationService.BookRoomAsync(roomId, userId.Value, fromDate, toDate);

            // Redirect to ReservationStatus page after booking
            return RedirectToAction("ReservationStatus", "ReservationStatus");
        }
    }
}
