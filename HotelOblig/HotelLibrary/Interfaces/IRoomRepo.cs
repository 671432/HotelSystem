using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Interfaces
{
    public interface IRoomRepo : IRepo<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByAvailabilitty(string isAvailable);
    }
}
