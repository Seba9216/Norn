using Norn.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Norn.Models.Entities;

public class Room
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public List<int>? TimeIntervalIds { get; set; }

    public List<TimeInterval> TimeIntervals { get; set; }
    
    public List<int>? BookingIds { get; set; }
    
    public List<Booking> Bookings { get; set; }
    
    public int OpenTimeId { get; set; }

    public OpenTime OpenTime { get; set; }
    
    public TimeLease TimeLease { get; set; }
    
    [Range(0, 60)]
    public byte? Increment { get; set; }
    
    public List<int> OrginisationIds { get; set; }

    public List<Organisation> Organisations { get; set; }

}
