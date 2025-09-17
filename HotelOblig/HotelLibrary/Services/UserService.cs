using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            this._userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public async System.Threading.Tasks.Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object is empty (null).");
            //}else if(user.Role == null)
            //{
            //    throw new ArgumentNullException(nameof(user), "User did not get assigned a role.");
            }

            await _userRepo.AddAsync(user);

        }

        //public async void CreateUser (String Firstname, String Lastname, String Phone)
        //{
        //    User x = new();
        //    x.Name = Firstname;
        //    x.Surname = Lastname;
        //    x.Phone = Phone;

        //    await AddUserAsync(x);

        //}

        public async System.Threading.Tasks.Task DeleteUserAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist.");
            }

            await _userRepo.DeleteAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist.");
            }

            return user;
        }

        public async Task<User> GetUserByPhoneAsync(string phone)
        {
            return await _userRepo.GetByPhoneAsync(phone);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            
            var userByRole = await _userRepo.GetByRoleAsync(role);
            if( userByRole == null ) 
            { 
                throw new ArgumentException($"No users with '{role}' as role was found."); 
            }
            return userByRole;
        }

        public async System.Threading.Tasks.Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object is null.");
            }

            await _userRepo.UpdateAsync(user);
        }

        async System.Threading.Tasks.Task IUserService.CreateUser(string Firstname, string Lastname, string Phone)
        {
            User x = new();
            x.Name = Firstname;
            x.Surname = Lastname;
            x.Phone = Phone;

            await AddUserAsync(x);
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _userRepo.getByNameAsync(username);
        }

        async Task<User> IUserService.GetUserByNameAsync(string username)
        {
            return await _userRepo.getByNameAsync(username);
        }
    }
}
