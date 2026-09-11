using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Organisation : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public List<OrganisationRoom> OrganisationRooms { get; set; }
}
