using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;
using Upp1_admin.Models;
using Upp1_admin.Services.Identity;

namespace Upp1_admin.Controllers
{


    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityService _identityService;
      

        public HomeController(
            ILogger<HomeController> logger, 
            IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }


        [AllowAnonymous]
        public   IActionResult Index()
        {
            //await _identityService.CreateRootAccountAsync();
            if (!_identityService.GetAllUsers().Any())
                return RedirectToAction("Index", "Installation");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
