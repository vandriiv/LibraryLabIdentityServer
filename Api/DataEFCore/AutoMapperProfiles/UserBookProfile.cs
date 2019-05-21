using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEFCore.AutoMapperProfile
{
    public class UserBookProfile : Profile
    {
        public UserBookProfile()
        {
            CreateMap<Entities.UserBook, UserBook>().ReverseMap();
        }
    }
}
