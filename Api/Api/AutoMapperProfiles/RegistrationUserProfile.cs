using AutoMapper;
using Domain.Models;
using LibraryLab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.AutoMapperProfiles
{
    public class RegistrationUserProfile : Profile
    {
        public RegistrationUserProfile()
        {
            CreateMap<UserRegistrationViewModel, User>();
        }
    }
}
