using Norn.Models.Entities;
using Norn.Models.Enums;

namespace Norn.Models.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wensday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public TimeLease TimeLease { get; set; }
    public byte Increment { get; set; }
    public byte? FromHour { get; set; }

    public byte? ToHour { get; set; }

    public List<OrgnisationRelation>? Organisations { get; set; }
    //TODO husk booking og time intervals
}
