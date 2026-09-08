using Norn.Models.Enums;

namespace Norn.Models.Models.Requests;

public class CreateRoomRequest
{
    public required string Name { get; set; }
    public required bool Monday { get; set; }
    public required bool Tuesday { get; set; }
    public required bool Wensday { get; set; }
    public required bool Thursday { get; set; }
    public required bool Friday { get; set; }
    public required bool Saturday { get; set; }
    public required bool Sunday { get; set; }
    public TimeLease TimeLease { get; set; }
    public required byte Increment { get; set; }
    public required byte? FromHour { get; set; }
    public required byte? ToHour { get; set; }
    public List<int> OrganisationIds { get; set; }
}
