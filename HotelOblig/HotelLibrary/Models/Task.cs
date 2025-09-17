using System;
using System.Collections.Generic;

namespace HotelLibrary.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int RoomNr { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public virtual Room RoomNrNavigation { get; set; } = null!;
}
