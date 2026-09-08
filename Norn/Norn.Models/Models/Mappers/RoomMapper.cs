
using Norn.Models.Entities;

namespace Norn.Models.Models.Mappers;

public static class RoomMapper
{

    public  static Models.Room MapToModel(Entities.Room room, List<OrgnisationRelation>? relations)
    {
        return new Room
        {
            Id = room.Id,
            Friday = room.Friday,
            Thursday = room.Thursday,
            Wensday = room.Wednesday,
            Tuesday = room.Tuesday,
            Monday = room.Monday,
            Increment = room.Increment,
            Name = room.Name,
            Saturday = room.Saturday,
            Sunday = room.Sunday,
            TimeLease = room.TimeLease,
            FromHour = room.FromHour,
            ToHour = room.ToHour,
            Organisations = relations
        };
    }
    public static Models.OrgnisationRelation MapToModel(Entities.Organisation organisation)
    {
        return new OrgnisationRelation
        {
            id = organisation.Id,
            OrganisationName = organisation.Name
        };
    }
}
