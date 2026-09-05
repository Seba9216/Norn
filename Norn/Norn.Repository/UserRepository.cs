using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;
using System.Data;

namespace Norn.Repository;

public class UserRepository : ListingRepo<User>, IUserRepository
{
    private NornContext _context;
    private IRoleRepository _roleRepository;
    public UserRepository(NornContext context, IRoleRepository roleRepository): base(context)
    {
        _context = context;
        _roleRepository = roleRepository;
    }
    public async Task<Models.Models.User?> GetUserByEmail(string email)
    {
        var result = await GetEntityByEmail(email);
        if (result != null)
        {
            var role = await _context.Roles.SingleAsync(x => x.Id == result.RoleId);
            return UserMapper.MapToModel(result.Email, result.Password, role.RoleName);
        }
        else
        {
            return null;
        }
    }

    private async Task<User?> GetEntityByEmail(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<Models.Models.User>> GetAllUsers()
    {
        var result = await GetAllEntitiesFromTable();
        var resultAsModels = result.Select(x =>
        {
            var role = _context.Roles.Single(entity => entity.Id == x.RoleId);
            return UserMapper.MapToModel(x.Email, x.Password, role.RoleName);
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

    public async Task<bool> DeleteUserByEmail(string email)
    {
        var userToRemove = await GetEntityByEmail(email);
        if (userToRemove != null)
        {
            _context.Users.Remove(userToRemove);
            await _context.SaveChangesAsync();
            return true; 
        }
        return false;
    }

    public async Task<Models.Models.User> UpdateRoleForUser(PromoteUserRequest userRequest)
    {
        var roleToUpdateTo = await _roleRepository.GetRoleByName(userRequest.role);
        if(roleToUpdateTo is null)
        {
            throw new InvalidOperationException("Role does not exist");
        }
        var userToPromote = await GetEntityByEmail(userRequest.email);
        if(userToPromote == null)
        {
            throw new InvalidOperationException("User does not exist");
        }
        userToPromote.RoleId = roleToUpdateTo.Id;
        await _context.SaveChangesAsync();
        return UserMapper.MapToModel(userToPromote.Email, userToPromote.Password, roleToUpdateTo.RoleName);
    }



}
