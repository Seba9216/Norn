using Norn.Models.Entities;

namespace Norn.Models.Models.Mappers;

public static class OrganisationMapper
{
    public static Models.Organisation MapToModel(int id, string name, List<Room>? rooms)
    {
        return new Models.Organisation { Id = id, Name = name, Rooms = rooms };
    }
}
