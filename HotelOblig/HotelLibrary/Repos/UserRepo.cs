using HotelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelLibrary.Interfaces;

namespace HotelLibrary.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly HotelContext dx;
        public UserRepo(HotelContext dx)
        {
             this.dx = dx;
        }


        public async System.Threading.Tasks.Task AddAsync(Models.User entity)
        {
            dx.User.Add(entity);
            await dx.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var userToDelete = await dx.User
                .FirstOrDefaultAsync(user => user.UserId == id);

            if (userToDelete != null)
            {
                dx.User.Remove(userToDelete);
                await dx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dx.User.ToListAsync();
            
        }

        public async Task<User>? GetByIdAsync(int id)
        {
           return await dx.User.FirstOrDefaultAsync(user => user.UserId == id);
 
        }

        //this one returns 1 user, might be fucked if we have more with same name but whatever.
        public async Task<User> getByNameAsync(string username)
        {
            return await dx.User.FirstOrDefaultAsync(user => user.Name == username);
        }

        public async Task<User>? GetByPhoneAsync(String phone)
        {
            return await dx.User.FirstOrDefaultAsync(user => user.Phone ==  phone);

        }
        public async Task<IEnumerable<User>>? GetByRoleAsync(string role)
        {
               return await dx.User.Where(user => user.Role == role).ToListAsync();
        }

        public Task<IEnumerable<User>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task UpdateAsync(User entity)
        {
            dx.User.Update(entity);
            await dx.SaveChangesAsync();
        }
    }
}
