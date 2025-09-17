using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Models.User>> GetAllUsersAsync();
        Task<Models.User> GetUserByIdAsync(int userId);
        Task<Models.User> GetUserByPhoneAsync(string phone);
        Task<IEnumerable<Models.User>> GetUsersByRoleAsync(string role);
        Task AddUserAsync(Models.User user);
        Task UpdateUserAsync(Models.User user);
        Task DeleteUserAsync(int userId);

        Task CreateUser(String Firstname, String Lastname, String Phone);
        Task<Models.User> GetUserByNameAsync(string username);
    }
}
