using Norn.Models.Models.Requests;

namespace Norn.Repository;

public interface IRoomRepository
{
    public Task<Models.Models.Room> CreateRoom(CreateRoomRequest request);
    public Task<List<Models.Models.Room>> GetAllRooms();
    public Task<List<int>> GetAllRelatedOrgs(int id);
    public Task<Models.Models.Room> UpdateByRoom(UpdateRoomRequest request);
    public Task<bool> DeleteRoom(int id);
}