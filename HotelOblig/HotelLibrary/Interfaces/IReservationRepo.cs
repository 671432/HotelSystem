using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Interfaces
{
    public interface IReservationRepo : IRepo<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationByUserIdAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByRoomId(int roomId);
        System.Threading.Tasks.Task CancleReservationAsync(int id);
        Task<List<Reservation>> GetAllReservationsIncudingAllAsync();



    }
}
    
