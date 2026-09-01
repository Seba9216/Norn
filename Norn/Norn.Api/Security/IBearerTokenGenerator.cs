using Norn.Models.Models;

public interface IBearerTokenGenerator
{
    public string GenerateToken(User user, string roleName);
}