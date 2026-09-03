using System;
using System.Collections.Generic;
using System.Text;

namespace Norn.Models.Models.Requests;

public record PromoteUserRequest(string email, string role);
