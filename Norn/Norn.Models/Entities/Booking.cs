using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Booking
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public int TimeIntervalId { get; set; }
    public int BookingStatusId { get; set; }
}
