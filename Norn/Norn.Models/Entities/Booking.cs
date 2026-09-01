using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Norn.Models.Entities;

internal class Booking
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public int TimeIntervalId { get; set; }
    public int BookingStatusId { get; set; }
}
