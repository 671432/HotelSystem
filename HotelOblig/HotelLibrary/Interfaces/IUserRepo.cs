using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Interfaces
{
    public interface IUserRepo : IRepo<User>
    {
        Task<User> getByNameAsync(string username);
        Task<User> GetByPhoneAsync(string phone);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
      
    }
}
