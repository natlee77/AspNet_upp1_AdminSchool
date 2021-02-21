using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;

namespace Upp1_admin.Models
{
    public class AdminUserViewModel:AppUser
    {
        public ICollection<string> Roles { get; set; }
    }
}
