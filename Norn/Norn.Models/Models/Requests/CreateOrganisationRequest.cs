namespace Norn.Models.Models.Requests;

public class CreateOrganisationRequest
{
    public required string Name { get; set; }
    public List<int>? RoomIds { get; set; }
}
