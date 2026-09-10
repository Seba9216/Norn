using Norn.Models.Entities;

namespace Norn.Models.Models.Mappers;

public static class OrganisationMapper
{
    public static Models.Organisation MapToModel(int id, string name, List<RoomRelation>? rooms)
    {
        return new Models.Organisation { Id = id, Name = name, Rooms = rooms };
    }
    public static Models.RoomRelation? MapToModel(Entities.OrganisationRoom room)
    {
        if (room != null)
        {
            return new RoomRelation
            {
                id = room.RoomId,
                RoomName = room.Room.Name
            };
        }
        return null;
    }

    public static Models.Organisation MapToModel(Entities.Organisation organisation)
    {
        var rooms = organisation.OrganisationRooms?.Select(or => MapToModel(or)).ToList();
        return new Models.Organisation
        {
            Id = organisation.Id,
            Name = organisation.Name,
            Rooms = rooms
        };
    }
}
