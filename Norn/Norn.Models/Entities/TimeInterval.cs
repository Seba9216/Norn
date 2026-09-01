using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Norn.Models.Entities;

internal class TimeInterval
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public ulong From { get; set; }
    public long To { get; set; }
    public int TimeIntervalTypeId { get; set; }
}
