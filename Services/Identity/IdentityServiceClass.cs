using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;
using Upp1_admin.Models;

namespace Upp1_admin.Services.Identity
{
    public class IdentityServiceClass : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public IdentityServiceClass(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task AddUserToRole(AppUser user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CreateNewRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> CreateNewUser(AppUser user, string password)//<IdentityResult>-retunera object tillbacka
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task CreateRootAccountAsync()//implemintera
        {


            //behöver CTOR
            if (!_userManager.Users.Any())  //skapa user-admin
            {
                var user = new AppUser()
                {
                    UserName = "admin@domain.com",
                    FirstName = "Admin",
                    LastName = "Account",
                    Email = "admin@domain.com"
                };
                var result = await _userManager.CreateAsync(user, "BytMig123!");

                if (result.Succeeded)
                {
                    if (!_roleManager.Roles.Any())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                        await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public IEnumerable<AppUser> GetAllUsers()
        {
           return _userManager.Users;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersWithRolesAsync()
        {
            //hämta alla user  , alla role och matcha ihåp
            var users = GetAllUsers(); //hämta alla user
            var userlist = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);  //hämta all roler for visa user
                var role = roles.FirstOrDefault();
                //skapa new object ---automapper


                userlist.Add(_mapper.Map<UserViewModel>(user));

            }
            return userlist;
        }

        public async Task<IdentityResult> CreateNewUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

    }
}// ++ i StartUp    -------->  services.AddScoped<IIdentityService, IdentityServiceClass>();
