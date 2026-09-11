
namespace Norn.Models.Models.Mappers;

public static class RoomMapper
{

    public  static Models.Room MapToModel(Entities.Room room)
    {
        var relations = room.OrganisationRooms?.Select(x => x.Organisation);
        var relationsResult = new List<OrganisationRelation>();
        if(relations is not null)
        {
            relationsResult = relations.Select(x => MapToModel(x)).ToList();
        }
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
            Organisations = relationsResult
        };
    }
    public static Models.RoomRelation MapToRelationModel(Entities.Room room)
    {
        return new RoomRelation
        {
            id = room.Id,
            RoomName = room.Name
        };
    }
    public static Models.OrganisationRelation? MapToModel(Entities.Organisation organisation)
    {
        if (organisation != null)
        {
            return new OrganisationRelation
            {
                id = organisation.Id,
                OrganisationName = organisation.Name
            };
        }
        return null;
    }
}
