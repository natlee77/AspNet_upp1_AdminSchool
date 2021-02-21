using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Upp1_admin.Data;

namespace Upp1_admin.Areas.Identity.Pages.Account
{
    [Authorize]  //bigransa user att logg in
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;//hantera användare
        private readonly RoleManager<IdentityRole> _roleManager;//hantera role

        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }//inputmodel --i garfisk design används

        public string ReturnUrl { get; set; }//om det fel i formular -->go back

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            //++
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "FirstName")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "LastName")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
            public string LastName { get; set; }



            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {


           
       
            //    return LocalRedirect("/");   //hur gör ? söka   asp.net redirect
            
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)//create user
            {
                var user = new AppUser { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    FirstName = Input.FirstName,//++
                    LastName = Input.LastName//++
                };               
                var result = await _userManager.CreateAsync(user, Input.Password);
                //created user



                if (result.Succeeded) //if succeess
                {
                    _logger.LogInformation("User created a new account with password.");



                    ////++role
                    //if (_userManager.Users.Count() == 1) //finns any role ? -om !-då skapa 2 role
                    //{
                    //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    //    await _roleManager.CreateAsync(new IdentityRole("User"));
                    //    await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                    //    await _roleManager.CreateAsync(new IdentityRole("Student"));

                    //    await _userManager.AddToRoleAsync(user, "Admin");
                    //}
                    //else
                    //    await _userManager.AddToRoleAsync(user, "Student");  //här måste skapa ändring roler --separat admin sida
                    ////som ++roler/skapa user---admin del omdirigera till


                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //om det true måste confirmera epost-adress-
                    //i startupp reguleras i services.--> false-true 
                    //false->behovs inte confirmera-logga automatisk
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(returnUrl);
                        return RedirectToAction("Index", "Home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // ViewData["roles"]= _roleManager.Roles.ToList();//dropdown list

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }

  


}
