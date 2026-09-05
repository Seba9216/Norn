using Norn.Models.Entities;

namespace Norn.Models.Models;

public class Organisation
{
    public int Id { get; set; }
    public required string Name;
    public List<Room>? Rooms { get; set; }
}
