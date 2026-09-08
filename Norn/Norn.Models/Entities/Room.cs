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
    
    public List<TimeInterval> TimeIntervals { get; set; }
    

    public List<Booking> Bookings { get; set; }

    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }

    [Range(0, 24)]
    public byte? FromHour { get; set; }
    [Range(0, 24)]
    public byte? ToHour { get; set; }


    public TimeLease TimeLease { get; set; }
    
    [Range(0, 60)]
    public byte Increment { get; set; }
    
    public List<OrganisationRoom> OrganisationRooms { get; set; }

}
