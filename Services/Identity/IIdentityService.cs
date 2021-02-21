using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;
using Upp1_admin.Models;

namespace Upp1_admin.Services.Identity
{
    public interface IIdentityService
    {
        Task CreateRootAccountAsync();

        Task CreateNewRole(string roleName);

         Task<IdentityResult> CreateNewUserAsync(AppUser user, string password);

        Task AddUserToRole(AppUser user, string roleName);

        IEnumerable<AppUser> GetAllUsers();
        IEnumerable<IdentityRole> GetAllRoles();

        Task<IEnumerable<UserViewModel>> GetAllUsersWithRolesAsync();
    }
}
