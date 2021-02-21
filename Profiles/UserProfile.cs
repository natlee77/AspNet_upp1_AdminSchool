using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upp1_admin.Data;
using Upp1_admin.Models;

namespace Upp1_admin.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, AdminUserViewModel>();
            CreateMap < AppUser, UserViewModel >();  //kan +++ flera views
        }
    }
}
