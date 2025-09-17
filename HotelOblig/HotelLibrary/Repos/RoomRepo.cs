using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Repos
{
    public class RoomRepo : IRoomRepo
    {
        private readonly HotelContext dx;
        public RoomRepo(HotelContext dx)
        {
            this.dx = dx;
        }

        public async System.Threading.Tasks.Task AddAsync(Room entity)
        {
            dx.Rooms.Add(entity);
            await dx.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var roomToDelete = await dx.Rooms.FirstOrDefaultAsync(room => room.RoomNr == id);

            if (roomToDelete != null)
            {
                dx.Rooms.Remove(roomToDelete);
                await dx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            var rooms = await dx.Rooms.ToListAsync();
            return rooms;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            var getRoom = await dx.Rooms.FirstOrDefaultAsync(Room => Room.RoomNr == id);
            if (getRoom != null)
            {
                return getRoom;
            }
            return null;
        }

        public Task<IEnumerable<Room>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Room>> GetRoomsByAvailabilitty(string isAvailable)
        {
            return await dx.Rooms.Where(room => room.Status == isAvailable).ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Room entity)
        {
            dx.Rooms.Update(entity);
            await dx.SaveChangesAsync();
        }
    }
}
