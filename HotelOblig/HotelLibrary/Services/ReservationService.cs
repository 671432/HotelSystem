using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using HotelLibrary.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepo _reservationRepo;
        public ReservationService(IReservationRepo rs)
        {
            this._reservationRepo = rs;
        }

        public async System.Threading.Tasks.Task BookRoomAsync(int roomId, int userId, DateOnly fromDate, DateOnly toDate)
        {
            // Create a new reservation record
            var reservation = new Reservation
            {
                RoomNr = roomId,
                UserId = userId,
                FromDate = fromDate,
                ToDate = toDate,
                Status = "Reserved" // Assuming default status is "Reserved"
            };

            // Add the reservation to the database
            await _reservationRepo.AddAsync(reservation);
        }

        public async System.Threading.Tasks.Task AddReservationAsync(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            ValidateReservation(reservation);

            await _reservationRepo.AddAsync(reservation);
        }

        private void ValidateReservation(Reservation reservation)
        {
            //Sjekke UserID, roomID, (fraDato < tilDato)

            if (reservation.User.Phone == null || reservation.RoomNr == null)
                throw new ArgumentNullException("Mangler BrukerID eller RomNR");

            if (reservation.FromDate >= reservation.ToDate)
                throw new ArgumentException("Ugyldig booking periode");

        }


        //Endre til cancel reservation, må endre interface !
        public async System.Threading.Tasks.Task CancleReservationAsync(int reservationId)
        {
            if (reservationId == null) throw new ArgumentNullException(nameof(reservationId));

            await _reservationRepo.CancleReservationAsync(reservationId);
        }


        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationRepo.GetAllAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            if (reservationId == null) throw new ArgumentNullException("Mangler reservasjonsID");
            return await _reservationRepo.GetByIdAsync(reservationId);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            if (userId == null) throw new ArgumentNullException(nameof(userId), "User ID cannot be null.");
            return await _reservationRepo.GetListByIdAsync(userId);
        }

        public async System.Threading.Tasks.Task UpdateReservationAsync(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            ValidateReservation(reservation);


            await _reservationRepo.UpdateAsync(reservation);

        }
        public async Task<List<Reservation>> GetAllReservationsIncudingAllAsync()
        {
            return await _reservationRepo.GetAllReservationsIncudingAllAsync();

        }

        public async Task<IEnumerable<Reservation>> GetReservationsByDateRangeAsync(DateOnly fromDate, DateOnly toDate)
        {
            var allReservations = await _reservationRepo.GetAllReservationsIncudingAllAsync();

            return allReservations.Where(r => r.FromDate <= toDate && r.ToDate >= fromDate);
        }
    }
}
