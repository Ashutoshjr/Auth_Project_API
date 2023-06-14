using AuthProject.Application.DTOs;
using AuthProject.Domain;
using AuthProject.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<AuthenticateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FirstName, opt => opt.Ignore())
            .ForMember(dest => dest.LastName, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());


            CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());

            // Add more mappings as needed for other entities and DTOs
        }
    }
}
