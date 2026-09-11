using Microsoft.EntityFrameworkCore;
using Norn.Repository;

namespace TestProject1;

public class Tests
{
    private IRoleRepository _roleRepository;
    private IUserRepository _userRepository;
    private NornContext _context;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<NornContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new NornContext(options);

        _context.Roles.Add(new Norn.Models.Entities.Role { RoleName = "Pretorian", Id = 2 });
        _context.Roles.Add(new Norn.Models.Entities.Role { RoleName = "Emporer", Id = 1 });

        _context.Users.Add(new Norn.Models.Entities.User
        {
            Email = "IamBrutus@CesarRules.com",
            Password = "IhateCarthago",
            Id = 1,
            RoleId = 2,
        });
        _context.SaveChanges();
        _roleRepository = new RoleRepository(_context);
        _userRepository = new UserRepository(_context, _roleRepository);
    }
    [TearDown]
    public void Teardown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task TestPromotionShouldBeOK()
    {
        var userToBePromoted = await _userRepository.UpdateRoleForUser(new Norn.Models.Models.Requests.PromoteUserRequest ("IamBrutus@CesarRules.com", "Emporer"));
        Assert.That(userToBePromoted.Role == "Emporer");
    }
}
