using ASPCustomer.Models;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using HotelLibrary.Services;
using ASPCustomer.Helpers;

namespace ASPCustomer.Controllers
{
    public class RoomsListController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;

        public RoomsListController(IRoomService roomService, IReservationService reservationService)
        {
            _roomService = roomService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> RoomsList(int? beds, string quality, DateOnly? fromDate, DateOnly? toDate)
        {
            var rooms = await _roomService.GetAllRoomsAsync();

            // Check if user is logged in
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("CurrentUser");
            // Retrieving the UserId from the retrieved user object
            var userId = currentUser?.UserId;
            ViewData["UserId"] = userId;

            if (beds.HasValue)
            {
                rooms = rooms.Where(r => r.Beds == beds);
            }

            if (!string.IsNullOrEmpty(quality))
            {
                rooms = rooms.Where(r => r.Quality == quality);
            }

            if (fromDate.HasValue && toDate.HasValue)
            {
                var reservations = await _reservationService.GetReservationsByDateRangeAsync(fromDate.Value, toDate.Value);

                // Exclude rooms that are reserved during the specified period
                rooms = rooms.Where(r => !IsRoomReservedDuringPeriod(r, reservations, fromDate, toDate)).ToList();
            }

            return View(rooms);
        }

        public async Task<IActionResult> BookRoom(int roomId, DateOnly fromDate, DateOnly toDate)
        {
            // Check if user is logged in
            var currentUser = HttpContext.Session.GetObjectFromJson<User>("CurrentUser");
            // Retrieving the UserId from the retrieved user object
            var userId = currentUser?.UserId;
            ViewData["UserId"] = userId;
            if (!userId.HasValue)
            {
                // User is not logged in, redirect to login page
                return RedirectToAction("CustomLogin", "Account");
            }

            // Perform booking logic
            try
            {
                //await _reservationService.BookRoomAsync(roomId, userId.Value, fromDate, toDate);
                // Redirect to ReservationStatus page after booking
                return RedirectToAction("ReservationStatus", "ReservationStatus");
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                // You can log the error, display an error message, or redirect to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        private bool IsRoomReservedDuringPeriod(Room room, IEnumerable<Reservation> reservations, DateOnly? fromDate, DateOnly? toDate)
        {
            foreach (var reservation in reservations)
            {
                if (room.RoomNr == reservation.RoomNr &&
                    fromDate >= reservation.FromDate && toDate <= reservation.ToDate)
                {
                    return true; // Room is reserved during the specified period
                }
            }
            return false; // Room is available
        }
    }
}