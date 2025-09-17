using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepo roomRepo;
        private readonly IReservationRepo reservationRepo;
        public RoomService(IRoomRepo roomRepo, IReservationRepo reservationRepo)
        {
            this.roomRepo = roomRepo;
            this.reservationRepo = reservationRepo;
        }


        public async System.Threading.Tasks.Task CreateRoomAsync(Room room)
        {

            ValidateRoom(room);

            await roomRepo.AddAsync(room);
        }

        private void ValidateRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException("Rom er null");

            if (room.Beds <= 0) throw new ArgumentException("Antall senger må være større enn 0");

            if (room.Quality == null) throw new ArgumentException("Rommet må ha en kvalitet");
        }


        public async System.Threading.Tasks.Task DeleteRoomAsync(int id)
        {
            var activeReservationns = await reservationRepo.GetReservationsByRoomId(id);

            if (activeReservationns.Any()) throw new InvalidOperationException("Kan ikke slette et rom, som har en aktiv reservasjon");

            await roomRepo.DeleteAsync(id);


        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await roomRepo.GetAllAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Ugyldig rom nr");

            return await roomRepo.GetByIdAsync(id);
        }

        public async System.Threading.Tasks.Task UpdateRoomAsync(Room room)
        {
            if (room == null) throw new ArgumentNullException("Rom er null");
            if (room.Quality == null) throw new ArgumentException("Må angi en kvalitet på rom");
            if (room.Beds <= 0) throw new ArgumentException("Rom må ha mer enn 0 senger");
            await roomRepo.UpdateAsync(room);
        }
    }
}
