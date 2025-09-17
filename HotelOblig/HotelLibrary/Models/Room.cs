using System;
using System.Collections.Generic;

namespace HotelLibrary.Models;

public partial class Room
{
    public int RoomNr { get; set; }

    public int? Beds { get; set; }

    public string? Quality { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
