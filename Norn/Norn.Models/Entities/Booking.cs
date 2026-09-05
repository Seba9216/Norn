using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Booking
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    
    [ForeignKey("UserId")]
    public int UserId { get; set; }

    public User User { get; set; }
    
    [ForeignKey("RoomId")]
    public int RoomId { get; set; }

    public Room Room { get; set; }
    
    [ForeignKey("TimeIntervalId")]
    public int TimeIntervalId { get; set; }
    public TimeInterval TimeInterval { get; set; }

    [ForeignKey("BookingStatusId")]
    public int BookingStatusId { get; set; }
    public BookingStatus BookingStatus { get; set; }
}
