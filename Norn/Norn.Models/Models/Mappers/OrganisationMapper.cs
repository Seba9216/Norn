using Norn.Models.Entities;

namespace Norn.Models.Models.Mappers;

public static class OrganisationMapper
{
    public static Models.Organisation MapToModel(int id, string name, List<RoomRelation>? rooms)
    {
        return new Models.Organisation { Id = id, Name = name, Rooms = rooms };
    }
    public static Models.RoomRelation MapToModel(Entities.Room room)
    {
        return new RoomRelation
        {
            id = room.Id,
            RoomName = room.Name
        };
    }
}
