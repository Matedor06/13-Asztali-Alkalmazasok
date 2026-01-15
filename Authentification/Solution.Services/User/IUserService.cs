using ErrorOr;
using Solution.Domain.Models.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Services.User;

public interface IUserService
{
    Task<ErrorOr<ICollection<UserModel>>> GetAllUsers();
}
