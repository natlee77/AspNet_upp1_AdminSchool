using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;
using Upp1_admin.Models;

namespace Upp1_admin.Controllers
{

    [Authorize(Policy = "adminAccess")]
    public class AdminController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public AdminController(UserManager<AppUser> userManager,  RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;//-->users() -hämta users in lista 
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {          
            return View();
        }

        //lista ut users
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            //skapa new form utav object, som jag kan göra nånt://jag vill ser/add/packetera role
            var userlist = new List<AdminUserViewModel>();

            foreach(var user in users)
            {
                //1-skapa new Model=AdminUserViewModel//2.install automapåper/3. DI-mapper
                // + new object --/ automapper-perfect for nån andring nånstance
                var u=_mapper.Map<AdminUserViewModel>(user);
                u.Roles = await _userManager.GetRolesAsync(user);  //+ ha andra saker init          
                userlist.Add(u);
            }
            
            
           
            return View(userlist);
        }

        //lista ut roles
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }


        //skapa roles
        [Route("admin/users/create")]       
        [HttpGet]
        public IActionResult CreateRole()  
        {
              return View(new IdentityRole());
        }
        [Route("admin/users/create")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)   
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Roles");
        }
    }
}
