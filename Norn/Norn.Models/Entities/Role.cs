using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Role : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string RoleName { get; set; }
}
