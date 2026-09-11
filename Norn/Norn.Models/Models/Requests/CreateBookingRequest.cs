using System;
using System.Collections.Generic;
using System.Text;

namespace Norn.Models.Models.Requests;

public class CreateBookingRequest
{
    public int UserId { get; set; }
    public int TimeIntervalId { get; set; }
    public int RoomId { get; set; }
}
