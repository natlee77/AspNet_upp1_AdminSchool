using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;
using Upp1_admin.Models;
using Upp1_admin.Services.Identity;

namespace Upp1_admin.Controllers
{
    public class InstallationController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityService _identityService;


        public InstallationController(
            SignInManager<AppUser> signInManager,
            IIdentityService identityService)
        {
            _signInManager = signInManager;
            _identityService = identityService;
        }
        public async Task<IActionResult> Index()
        {

            if (!_identityService.GetAllUsers().Any())
                //return View();
                await _identityService.CreateRootAccountAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Index(InstallationViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = new AppUser
            //    {
            //        UserName = "admin@domain.com",
            //        Email = "admin@domain.com",
            //        FirstName = "Admin",
            //        LastName = "Account"
            //    };
            //    var result = await _identityService.CreateNewUserAsync(user, model.Password);


            //    if (result.Succeeded)
            //    {
            //        await _identityService.CreateNewRole .CreateAsync(new IdentityRole("Admin"));
            //        await _roleManager.CreateAsync(new IdentityRole("Programm Manager"));
            //        await _roleManager.CreateAsync(new IdentityRole("Teacher"));
            //        await _roleManager.CreateAsync(new IdentityRole("Student"));

            //        await _userManager.AddToRoleAsync(user, "Admin");
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        return RedirectToAction("Index", "Home");
            //    }

            //            foreach (var error in result.Errors)
            //            {
            //                ModelState.AddModelError(string.Empty, error.Description);
            //            }
            //}
            return View();
        }

    }
}
