using AutoMapper;
using Domain.Models;
using LibraryLab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.AutoMapperProfiles
{
    public class NewBookViewModelProfile : Profile
    {
        public NewBookViewModelProfile()
        {
            CreateMap<NewBookViewModel, Book>();
        }
    }
}
