using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;

namespace Norn.Repository;

public class RoomRepository : ListingRepo<Room>, IRoomRepository
{
    NornContext _nornContext;
    public RoomRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }
    public async Task<Models.Models.Room> CreateRoom(CreateRoomRequest request)
    {
        var entity = new Room
        {
            Name = request.Name,
            Monday = request.Monday,
            Tuesday = request.Tuesday,
            Wednesday = request.Wensday,
            Thursday = request.Thursday,
            Friday = request.Friday,
            FromHour = request.FromHour,
            ToHour = request.ToHour,
            Saturday = request.Saturday,
            Sunday = request.Sunday,
            TimeLease = request.TimeLease,
            Increment = request.Increment,
        };

        _nornContext.Rooms.Add(entity);


        await _nornContext.SaveChangesAsync();
        if (request.OrganisationIds != null)
        {
            foreach (var org in request.OrganisationIds)
            {
                var foundorg = await _nornContext.Organisations.SingleOrDefaultAsync(x => x.Id == org);
                if (foundorg is not null)
                {
                    _nornContext.OrganisationRoom.Add(new OrganisationRoom
                    {
                        OrganisationId = foundorg.Id,
                        RoomId = entity.Id
                    });
                }
                await _nornContext.SaveChangesAsync();
            }
        }

        if (entity.OrganisationRooms != null)
        {
            return RoomMapper.MapToModel(entity, entity.OrganisationRooms.Select(x =>
            {
                return RoomMapper.MapToModel(x.Organisation);
            }).ToList());
        }
        else return RoomMapper.MapToModel(entity, null);
    }
    public async Task<List<Models.Models.Room>> GetAllRooms()
    {
        var result = await GetAllEntitiesFromTable();
        return result.Select(x =>
        {
            if (x.OrganisationRooms != null)
            {
                return RoomMapper.MapToModel(x, x.OrganisationRooms.Select(x =>
                    {
                        if (x.Organisation != null)
                        {
                            return RoomMapper.MapToModel(x.Organisation);
                        }
                        return null;
                    }).ToList());

                 
            }
            else {
                return RoomMapper.MapToModel(x, null);
                };
        }).ToList();
    }
    public async Task<List<int>> GetAllRelatedOrgs(int id)
    {
        return await _nornContext.OrganisationRoom.Where(x => x.RoomId == id).Select(x => x.OrganisationId).ToListAsync();
    }
}
