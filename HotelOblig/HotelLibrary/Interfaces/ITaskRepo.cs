using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Interfaces
{
    public interface ITaskRepo : IRepo<Models.Task>
    {
        Task<IEnumerable<Models.Task>> GetTaskByStatusAsync(string status);
    }
}
