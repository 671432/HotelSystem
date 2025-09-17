using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Models.Room>> GetAllRoomsAsync();
        Task<Models.Room> GetRoomByIdAsync(int id);
        Task CreateRoomAsync(Models.Room room);
        Task UpdateRoomAsync(Models.Room room);
        Task DeleteRoomAsync(int id);
    }
}
