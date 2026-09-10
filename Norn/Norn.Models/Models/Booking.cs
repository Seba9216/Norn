namespace Norn.Models.Models;

internal class Booking
{
    public int Id { get; set; }
    public User User { get; set;  }
    public RoomRelation Room { get; set; }
    public TimeIntervalRelation TimeInterval { get; set; }
}
