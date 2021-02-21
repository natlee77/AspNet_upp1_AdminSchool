using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Upp1_admin.Data
{
    public class AppUserClaims : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        

            
            //ctor
            public AppUserClaims(
                UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IOptions<IdentityOptions> options)
                : base(userManager, roleManager, options)//abstract-i C#/skicka managers tillbacka ->base UserClaimsPrincipalFactory
            {
                
            }
            //override -det skapar id , username/som kan!bryta,bara lägga till de 
            protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
            {
                var roles = await UserManager.GetRolesAsync(user);//list roles som user ska hämta

                var _identity = await base.GenerateClaimsAsync(user);               

                //++extra claims   --key<->value par
                _identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));//om det tomt?? generera tomt
                _identity.AddClaim(new Claim("LastName", user.LastName ?? ""));               
                _identity.AddClaim(new Claim("DisplayName", user.GetDisplayName() ?? ""));
                //_identity.AddClaim(new Claim(ClaimTypes.GivenName, user.LastName ?? ""));

                _identity.AddClaim(new Claim(ClaimTypes.Role, roles.FirstOrDefault()));

            return _identity;
            }
        
    }
}
