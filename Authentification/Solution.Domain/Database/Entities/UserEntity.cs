using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Domain.Database.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public string FullName { get; set; }

    public DateTime RegisteredAtUtc { get; init; } = DateTime.UtcNow;
}
