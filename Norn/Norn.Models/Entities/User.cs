
using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class User 
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Email { get; set; }
    [ForeignKey("RoleId")]
    public required int RoleId { get; set; }
    public required string Password { get; set; }
    public List<int> BookingIds { get; set; }
    public List<Booking> Bookings { get; set; }

}
