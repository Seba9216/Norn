using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Enums;
using Norn.Models.Models.Mappers;

namespace Norn.Repository;

public class TimeIntervalRepository : ListingRepo<TimeInterval>, ITimeIntervalRepository
{
    private NornContext _nornContext;
    private byte _yearsToCalculate = 1;
    private byte _openingHourDefault = 0;
    private byte _closingHourDefault = 24;

    public TimeIntervalRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }
    public async Task<List<TimeInterval>>UpdateTimesForRoom(Room room)
    {
        var intervalsToRemove = await _nornContext.TimeIntervals.Where(x => x.RoomId == room.Id).ToListAsync();
        _nornContext.TimeIntervals.RemoveRange(intervalsToRemove);
        await _nornContext.SaveChangesAsync();
        return await CreateTimeSchemaFromRoom(room);
    }
    
    
    /// <summary>
    /// Pr default this methods makes times too book for a year at the time.
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    

    public async Task<List<TimeInterval>> CreateTimeSchemaFromRoom(Room room)
    {
        var currentDate = DateTime.Today;
        var endDate = currentDate.AddYears(_yearsToCalculate);

        var intervals = new List<TimeInterval>();

        foreach (var date in Enumerable.Range(0, (endDate - currentDate).Days).Select(i => currentDate.AddDays(i)))
        {
            if (!IsValidDay(room, date))
                continue;
            switch (room.TimeLease)
            {
                case TimeLease.Minutes:
                    {
                        var start = date.AddHours(room.FromHour ?? _openingHourDefault);
                        var end = date.AddHours(room.ToHour ?? _closingHourDefault);

                        while (start < end)
                        {
                            var next = start.AddMinutes(room.Increment);

                            if (next > end)
                                break;

                            intervals.Add(new TimeInterval
                            {
                                RoomId = room.Id,
                                From = DateTime.SpecifyKind(start,DateTimeKind.Utc),
                                To = DateTime.SpecifyKind(next, DateTimeKind.Utc),
                            });
                            start = next;
                        }
                        break;
                    }

                case TimeLease.Hours:
                    {
                        var start = date.AddHours(room.FromHour ?? _openingHourDefault);
                        var end = date.AddHours(room.ToHour ?? _closingHourDefault);

                        while (start < end)
                        {
                            var next = start.AddHours(room.Increment);

                            if (next > end)
                                break;

                            intervals.Add(new TimeInterval
                            {
                                RoomId = room.Id,
                                From = DateTime.SpecifyKind(start, DateTimeKind.Utc),
                                To = DateTime.SpecifyKind(next, DateTimeKind.Utc),
                            });
                            start = next;
                        }

                        break;
                    }

                case TimeLease.Days:
                    {
                        intervals.Add(new TimeInterval
                        {
                            RoomId = room.Id,
                            From = date.Date.ToUniversalTime().AddHours(room.FromHour ?? _openingHourDefault),
                            To = date.Date.AddDays(1).ToUniversalTime().AddHours(room.ToHour ?? _closingHourDefault)
                        }); 
                        break;
                    }
            }
        }
        await _nornContext.AddRangeAsync(intervals);
        return intervals;
    }
    public async Task<List<Models.Models.TimeIntervalRelation>> GetRoomRelatedTimeIntervals(int roomId)
    {
         return await _nornContext.TimeIntervals.Where(x => x.RoomId == roomId).Select(x => TimeIntervalMapper.MapToModel(x)).ToListAsync();
    } 

    private bool IsValidDay(Room room, DateTime date)
    {
        return date.DayOfWeek switch
        {
            DayOfWeek.Monday => room.Monday,
            DayOfWeek.Tuesday => room.Tuesday,
            DayOfWeek.Wednesday => room.Wednesday,
            DayOfWeek.Thursday => room.Thursday,
            DayOfWeek.Friday => room.Friday,
            DayOfWeek.Saturday => room.Saturday,
            DayOfWeek.Sunday => room.Sunday,
            _ => false
        };
    }


}
