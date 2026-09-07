using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class BookingStatus
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Status { get; set; } 
    public List<Booking>? Bookings { get; set; }
}
