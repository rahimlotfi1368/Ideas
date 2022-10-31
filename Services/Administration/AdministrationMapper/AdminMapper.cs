using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Administration.Dtos;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Administration;

namespace Services.Administration.AdministrationMapper
{
    public class AdminMapper:Profile
    {
        public AdminMapper()
        {
            CreateMap<RoleViewModel, Role>().ForMember(des=>des.Name,des=>des.MapFrom(s=>s.RoleName));

            CreateMap<EditUserViewModel, User>();//.ForMember(d=>d.ProfileImage,d=>d.MapFrom(s=>Utilites.UploadFile(s.Images,s.)));
            
            CreateMap<IdentityRole,Role>();

            CreateMap<User, UserOutputDto>();
        }
    }
}
