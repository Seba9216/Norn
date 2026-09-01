using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class BookingStatus
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Satuts { get; set; } 
}
