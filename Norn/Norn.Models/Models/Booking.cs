using Norn.Models.Entities;

namespace Norn.Models.Models;

public class Booking
{
    public int Id { get; set; }
    public UserRelation User { get; set;  }
    public RoomRelation Room { get; set; }
    public TimeIntervalRelation TimeInterval { get; set; }
    public BookingStatusRelation BookingStatus { get; set; }
}
