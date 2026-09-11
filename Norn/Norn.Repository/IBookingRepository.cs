using Norn.Models.Models.Requests;

namespace Norn.Repository;

public interface IBookingRepository
{
    public Task<List<Models.Models.Booking>> GetAllBookings();

    public Task<Models.Models.Booking?> CreateBookingRequest(CreateBookingRequest request);
    public Task<Models.Models.Booking?> ApproveBookingById(int id);
    public Task<Models.Models.Booking?> CancelBookingById(int id);


}