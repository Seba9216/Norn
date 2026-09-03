using System.ComponentModel.DataAnnotations;

namespace Norn.Models.Models.Requests;

public class LoginRequest
{
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    public required string Email { get; set; }
    public required string Password { get; set; }
};
