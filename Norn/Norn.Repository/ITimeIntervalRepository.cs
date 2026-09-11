using Norn.Models.Entities;

namespace Norn.Repository;

public interface ITimeIntervalRepository
{
    public Task<List<TimeInterval>> CreateTimeSchemaFromRoom(Room room);
    public Task<List<Models.Models.TimeIntervalRelation>> GetRoomRelatedTimeIntervals(int roomId);

    public Task<List<TimeInterval>> UpdateTimesForRoom(Room room);
    public Task<int?> GetIdFromTimeAndRoomId(DateTime from, DateTime to, int roomId);
    public Task<bool> DoesTimeIntervalHaveBooking(int id);


}