using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Room
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }

}
