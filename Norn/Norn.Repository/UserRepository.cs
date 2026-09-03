using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using System.Data;

namespace Norn.Repository;

public class UserRepository : ListingRepo<User>, IUserRepository
{
    private NornContext _context;

    public UserRepository(NornContext context): base(context)
    {
        _context = context;
    }
    public async Task<Models.Models.User?> GetUserByEmail(string email)
    {
        var result = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email);
        if (result != null)
        {
            var role = await _context.Roles.SingleAsync(x => x.Id == result.RoleId);
            return new Models.Models.User
            {
                Email = result.Email,
                Password = result.Password,
                Role = role.RoleName.ToString()
            };
        }
        else
        {
            return null;
        }
    }

    public async Task<List<Models.Models.User>> GetAllUsers()
    {
        var result = await GetAllEntitiesFromTable();
        var resultAsModels = result.Select(x =>
        {
            var role = _context.Roles.Single(entity => entity.Id == x.RoleId);
            return new Models.Models.User
            {
                Email = x.Email,
                Password = x.Password,
                Role = role.RoleName.ToString()
            };
        }).ToList();
        return resultAsModels;
    }

    /// <summary>
    /// If there are no users in the current system the first user is created as an admin user, otherwise the user is created with the role úser.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> CreateUser(Models.Models.User user)
    {
        var role = await _context.Roles.SingleAsync(x => x.RoleName == user.Role);
        var isThereAnyUser = await _context.Users.AnyAsync();
        role = isThereAnyUser ? role : await _context.Roles.SingleAsync(x => x.RoleName == "Admin");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 12);

        var doesEmailExsist = await _context.Users.AnyAsync(x => x.Email == user.Email);

        if (doesEmailExsist) return false;

        var newUser = new User
        {
            Email = user.Email,
            Password = user.Password,
            RoleId = role.Id
        };
        _context.Users.Add(newUser);
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }




}
