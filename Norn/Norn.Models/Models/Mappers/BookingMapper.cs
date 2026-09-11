namespace Norn.Models.Models.Mappers;

public static class BookingMapper
{
    public static Models.Booking MapToModel(Entities.Booking booking)
    {
        return new Booking
        {
            Id = booking.Id,
            Room = RoomMapper.MapToRelationModel(booking.Room),
            TimeInterval = TimeIntervalMapper.MapToModel(booking.TimeInterval),
            User = UserMapper.MapToReltaionModel(booking.User),
             BookingStatus = new BookingStatusRelation { CurrentStatus = booking.BookingStatus.Status  }
        };
    }
}
