using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEFCore.AutoMapperProfile
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Entities.Genre, Genre>();
        }
    }
}
