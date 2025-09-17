using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelLibrary.Models;

namespace HotelLibrary.Interfaces
{
    public interface IReservationService
    {

        System.Threading.Tasks.Task BookRoomAsync(int roomId, int userId, DateOnly fromDate, DateOnly toDate);
        Task<IEnumerable<Models.Reservation>> GetAllReservationsAsync();
        Task<Models.Reservation> GetReservationByIdAsync(int reservationId);
        System.Threading.Tasks.Task AddReservationAsync(Models.Reservation teservation);
        System.Threading.Tasks.Task UpdateReservationAsync(Models.Reservation reservation);
        System.Threading.Tasks.Task CancleReservationAsync(int reservationId);
        Task<List<Reservation>> GetAllReservationsIncudingAllAsync();
        Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateOnly fromDate, DateOnly toDate);
        Task<IEnumerable<Models.Reservation>> GetReservationsByUserIdAsync(int userId);


    }
}
