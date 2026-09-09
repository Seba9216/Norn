using Norn.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Norn.Models.Entities;

public class TimeInterval
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
    
    public int RoomId { get; set; }
    public Room Room { get; set; }

    public List<Booking>? Bookings { get; set; }
}
