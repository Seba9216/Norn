using Norn.Models.Entities;

namespace Norn.Repository;

internal class TimeIntervalRepository : ListingRepo<TimeInterval>, ITimeIntervalRepository
{
    private NornContext _nornContext;
    public TimeIntervalRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }


}
