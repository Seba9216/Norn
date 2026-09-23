namespace Norn.Models.Models.Mappers;

public static class UserMapper
{
    public static Models.User MapToModel(string email, string password, string roleName)
    {
        return new Models.User
        {
            Email = email,
            Password = password,
            Role = roleName
        };
    }
    
    public static Models.UserRelation MapToReltaionModel(Entities.User user) 
    {
        return new UserRelation
        {
            Email = user.Email,
            Id = user.Id
        };
    }

}
