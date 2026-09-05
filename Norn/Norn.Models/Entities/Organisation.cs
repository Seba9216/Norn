using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Organisation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public List<int>? RoomIds { get; set; } 
    public List<Room>? Rooms { get; set; }
}
