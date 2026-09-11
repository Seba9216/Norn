
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Norn.Api.Controllers;
using Norn.Models.Models.Requests;
using Norn.Repository;

namespace Norn.IntegrationTests;

internal class UserControllerTests
{
    private Mock<IBearerTokenGenerator> _bearerTokenGenerator;
    private Mock<IUserRepository> _userRepository;
    private UserController _userController; 
    [SetUp]
    public void Setup()
    {
        _bearerTokenGenerator = new Mock<IBearerTokenGenerator>();
        _userRepository = new Mock<IUserRepository>();

        _userController = new UserController(_bearerTokenGenerator.Object, _userRepository.Object);
    }

    [TearDown]
    public void Teardown()
    {
        _userController.Dispose();
    }
    [Test]

    public async Task ControllerShouldReturnUserOK()
    {
        var request = new PromoteUserRequest("IamBrutus@CesarRules.com", "Emporer");
        _userRepository.Setup(x => x.UpdateRoleForUser(request)).ReturnsAsync(new Models.Models.User());
        var result = await _userController.PromoteUser(request);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
}
