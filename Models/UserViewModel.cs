using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;

namespace Upp1_admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]        
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]      
        public string Role { get; set; }
        public ICollection<string> Roles { get; set; }
        public string GetDisplayName()
        {
            return $"{FirstName} {LastName}";
        }
        
    }
}
