namespace Norn.Models.Models.Mappers;

public static class TimeIntervalMapper
{
    readonly static int confirmedStatus = 2;


    public static TimeIntervalRelation MapToModel(Entities.TimeInterval entity)
    {
        bool anyBookings = false;
        if (entity.Bookings is not null)
        {
           anyBookings = entity.Bookings.Where(x => x.BookingStatusId == confirmedStatus).Any();
        }
        return new TimeIntervalRelation
        {
            From = entity.From,
            To = entity.To,
            RoomId = entity.RoomId,
            IsBooked = anyBookings
        };
    }
}
