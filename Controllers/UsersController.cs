using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    //[Authorize(Policy = "adminAccess")]
    //[Route("admin/[controller]/[action]")]
     [Authorize(Roles = "Admin")]
    //[Route("/User")]
    public class UsersController : Controller
    {

        //++hämta alla users
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;



        public UsersController(IIdentityService identityService, UserManager<AppUser> userManager,  IMapper mapper)
        {
            _identityService = identityService;
            _userManager = userManager;
            _mapper = mapper;
        }


        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            ViewBag.Users = await _identityService.GetAllUsersWithRolesAsync();
            ViewBag.Roles = _identityService.GetAllRoles();
            ViewBag.Admins = await _userManager.GetUsersInRoleAsync("Admin");
            ViewBag.Teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            ViewBag.Students = await _userManager.GetUsersInRoleAsync("Student");
            return View(); 

            //var userlist = new List<UserViewModel>();  //list med   users 
            //var users = (IEnumerable<UserViewModel>)_identityService.GetAllUsers();

            //foreach (var user in users) //_userManager.Users
            //{
            //    var roles = _identityService.GetAllRoles();
            //    userlist.Add(new UserViewModel
            //    {
            //        Id = user.Id,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        Email = user.Email,
            //        Role = roles.FirstOrDefault().Id
            //    });


            //var users =  _identityService.GetAllUsers();
            //var userlist = new List<UserViewModel>();
            ////foreach (var user in users)
            ////{    var roles = _identityService.GetAllRoles();          
            ////    var u = _mapper.Map<UserViewModel>(user);
            ////    u.Role  =  roles.FirstOrDefault().Id;  //+ ha andra saker init   
            ////    _user.Roles = (ICollection<string>)roles.FirstOrDefault();  //+ ha andra saker init           
            ////    userlist.Add(u);
            ////}
            //return View(userlist);// ++ view på det
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: UsersController/Create
        public ActionResult Create()
        {                      
            ViewBag.Roles = _identityService.GetAllRoles();

            return View();
        }

        // POST: UsersController/Create
        [HttpPost]            
        public async Task<IActionResult> Create(UserViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = new AppUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    };
                    var result = await _identityService.CreateNewUserAsync(user, "BytMig123!");//model.Password



                    if (result.Succeeded)
                    {
                        await _identityService.AddUserToRole(user, model.Role);

                        return RedirectToAction("Index", "Users");
                    }


                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View();
            }
        

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
