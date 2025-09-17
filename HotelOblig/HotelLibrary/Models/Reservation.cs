    using System;
    using System.Collections.Generic;

    namespace HotelLibrary.Models;

    public partial class Reservation
    {
        public int ReservationId { get; set; }

        public int RoomNr { get; set; }

        public int UserId { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }

        public string? Status { get; set; }

        public virtual Room RoomNrNavigation { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
