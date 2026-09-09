namespace Norn.Models.Models.Mappers;

public static class TimeIntervalMapper
{
    public static TimeIntervalRelation MapToModel(Entities.TimeInterval entity)
    {
        return new TimeIntervalRelation
        {
            From = entity.From,
            To = entity.To,
            RoomId = entity.RoomId
            
        };
    }
}
