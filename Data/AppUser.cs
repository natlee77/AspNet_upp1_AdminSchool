using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Upp1_admin.Data
{
    public class AppUser : IdentityUser
    {


        [Required]//måste finnas ett värde -kan inte tomt
        [PersonalData]//när gör sökning i DB -märkt som PersonligDAta
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }


        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        //for claims
        [PersonalData]
        public string GetDisplayName()
        {
            return $"{FirstName} {LastName}";
        }


        
    }
}

