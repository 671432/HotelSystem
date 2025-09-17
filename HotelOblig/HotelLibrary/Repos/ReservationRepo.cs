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
    public class ReservationRepo : IReservationRepo
    {
        private readonly HotelContext dx;
        public ReservationRepo(HotelContext dx)
        {
            this.dx = dx;
        }



        public async System.Threading.Tasks.Task AddAsync(Reservation entity)
        {
            dx.Reservations.Add(entity);
            await dx.SaveChangesAsync();
        }



        public async System.Threading.Tasks.Task CancleReservationAsync(int id)
        {
            var cancleReservation = await dx.Reservations.FindAsync(id);
            if (cancleReservation != null)
            {
                cancleReservation.Status = "Cancled";
                await dx.SaveChangesAsync();
            }
        }



        public System.Threading.Tasks.Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }



        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await dx.Reservations.ToListAsync();
        }



        public async Task<Reservation>? GetByIdAsync(int id)
        {
            return await dx.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
        }



        public async Task<IEnumerable<Reservation>> GetReservationByUserIdAsync(int id)
        {
            return await dx.Reservations.Where(user => user.UserId == id).ToListAsync();
        }



        public async Task<IEnumerable<Reservation>> GetReservationsByRoomId(int roomId)
        {
            return await dx.Reservations
                .Where(r => r.RoomNr == roomId && r.Status == "Active")
                .ToListAsync();
        }



        public async System.Threading.Tasks.Task UpdateAsync(Reservation entity)
        {
            dx.Reservations.Update(entity);
            await dx.SaveChangesAsync();
        }
        
        public async Task<List<Reservation>> GetAllReservationsIncudingAllAsync()
        {
            return await dx.Reservations.Include(r => r.User).Include(r => r.RoomNrNavigation).ToListAsync();

        }

        public async Task<IEnumerable<Reservation>> GetListByIdAsync(int id)
        {
            return await dx.Reservations.Where(user => user.UserId == id).ToListAsync();
        }
    }
    }
