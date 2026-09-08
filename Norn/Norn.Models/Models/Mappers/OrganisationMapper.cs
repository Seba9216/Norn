using Norn.Models.Entities;

namespace Norn.Models.Models.Mappers;

public static class OrganisationMapper
{
    public static Models.Organisation MapToModel(int id, string name, List<RoomRelation>? rooms)
    {
        return new Models.Organisation { Id = id, Name = name, Rooms = rooms };
    }
    public static Models.RoomRelation? MapToModel(Entities.Room room)
    {
        if (room != null)
        {
            return new RoomRelation
            {
                id = room.Id,
                RoomName = room.Name
            };
        }
        return null;
    }

    public static Models.Organisation MapToModel(Entities.Organisation organisation)
    {
        var rooms = organisation.OrganisationRooms?.Select(or => MapToModel(or.Room)).ToList();
        return new Models.Organisation
        {
            Id = organisation.Id,
            Name = organisation.Name,
            Rooms = rooms
        };
    }
}
