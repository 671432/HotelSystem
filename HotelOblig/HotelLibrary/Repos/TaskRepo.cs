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
    public class TaskRepo : ITaskRepo
    {
        private readonly HotelContext dx;

        public TaskRepo(HotelContext dx)
        {
            this.dx = dx;
        }


        public async System.Threading.Tasks.Task AddAsync(Models.Task entity)
        {
            dx.Tasks.Add(entity);
            await dx.SaveChangesAsync();
        }


        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var hotelTask = await dx.Tasks.FindAsync(id);

            if (hotelTask != null)
            {
                dx.Tasks.Remove(hotelTask);
                await dx.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Models.Task>> GetAllAsync()
        {
            return await dx.Tasks.ToListAsync();
        }

        public async Task<Models.Task?> GetByIdAsync(int id)
        {
            return await dx.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);
        }

        public Task<IEnumerable<Models.Task>> GetListByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.Task>> GetTaskByStatusAsync(string status)
        {
            return await dx.Tasks.Where(x => x.Status == status).ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Models.Task entity)
        {
            dx.Tasks.Update(entity);
            await dx.SaveChangesAsync();
        }
    }
}