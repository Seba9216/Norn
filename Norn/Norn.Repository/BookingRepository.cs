using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Norn.Repository;

public class BookingRepository : ListingRepo<Booking>, IBookingRepository
{
    NornContext _nornContext;
    ITimeIntervalRepository _timeIntervalRepository;
    readonly int AwatingStatus = 1;
    readonly int ConfirmedStatus = 2;
    readonly int CanceldStatus = 3;
    public BookingRepository(NornContext context, ITimeIntervalRepository timeIntervalRepository) : base(context)
    {
        _nornContext = context;
        _timeIntervalRepository = timeIntervalRepository;
    }
    public async Task<List<Models.Models.Booking>> GetAllBookings()
    {
        var entities = await GetAllEntities(x => x.Include(x => x.User)
        .Include(x => x.Room)
        .Include(x => x.TimeInterval)
        .Include(x => x.BookingStatus)
        );
        var entitesAsModel = entities.Select(x => BookingMapper.MapToModel(x)).ToList();
        return entitesAsModel;
    }

    public async Task<Models.Models.Booking?> ApproveBookingById(int id)
    {
        var singularEnity = await GetAlllRealtionsById(id);
        if (singularEnity is null) return null;
        singularEnity.BookingStatusId = ConfirmedStatus;
        await _nornContext.SaveChangesAsync();
        singularEnity = await GetAlllRealtionsById(id);
        return BookingMapper.MapToModel(singularEnity);
    }
    public async Task<Models.Models.Booking?> CancelBookingById(int id)
    {
        Booking? singularEnity = await GetAlllRealtionsById(id);
        if (singularEnity is null) return null;
        singularEnity.BookingStatusId = CanceldStatus;
        await _nornContext.SaveChangesAsync();
        singularEnity = await GetAlllRealtionsById(id);
        return BookingMapper.MapToModel(singularEnity);
    }

    private async Task<Booking?> GetAlllRealtionsById(int id)
    {
        return await GetByPrimaryKey(id, x => x.Include(x => x.BookingStatus).Include(x => x.User).Include(x => x.TimeInterval).Include(x => x.Room).Include(x => x.BookingStatus));
    }

    public async Task<Models.Models.Booking?> CreateBookingRequest(CreateBookingRequest request)
    {
        try
        {
            var isthisBooked = await _timeIntervalRepository.DoesTimeIntervalHaveBooking(request.TimeIntervalId);
            if (isthisBooked)
            {
                return null; 
            }
            var entity = new Booking
            {
                UserId = request.UserId,
                TimeIntervalId = request.TimeIntervalId,
                RoomId = request.RoomId,
                BookingStatusId = AwatingStatus
            };

            _nornContext.Add(entity);
            await _nornContext.SaveChangesAsync();
            var sectedEntity = await _nornContext.Bookings.Include(x => x.Room).Include(x => x.User).Include(x => x.TimeInterval).Include(x => x.BookingStatus).SingleAsync(x => x.Id == entity.Id);

            return BookingMapper.MapToModel(entity);
        }
        catch
        {
            return null;
        }

    }


}
