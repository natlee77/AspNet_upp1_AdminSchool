using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;

namespace Upp1_admin.Models
{
    public class EditClassViewModel
    {
        public SchoolClass CurrentClass { get; set; }
        //public AppUser Teacher { get; set; }   //1
        public IEnumerable<AppUser> Teachers { get; set; }   //flera
        public string IsSelected { get; set; }

        //public ICollection<AppUser> Students {get; set;}
    }
}
