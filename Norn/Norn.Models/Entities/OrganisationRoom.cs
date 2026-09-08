namespace Norn.Models.Entities;

public class OrganisationRoom
{
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public int OrganisationId { get; set; }
    public Organisation Organisation { get; set; }
}
