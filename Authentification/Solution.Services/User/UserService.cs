using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Solution.Domain.Database.Entities;
using Solution.Domain.Models.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.Services.User
{
    public class UserService(UserManager<UserEntity> userManager) : IUserService
    {
        public async Task<ErrorOr<ICollection<UserModel>>> GetAllUsers()
        {
            return await userManager.Users.Select(x => new UserModel
            {
                Name = x.FullName,
                Email = x.Email
            }).ToListAsync();
        }
    }
}
