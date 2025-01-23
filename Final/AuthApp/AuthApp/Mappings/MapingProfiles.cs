using AuthApp.Models;
using AuthApp.Utils;
using AutoMapper;
using EF.Models;

namespace AuthApp.Mappings
{
    public class MapingProfiles: Profile
    {
        public MapingProfiles()
        {
            CreateMap<User, UserReadViewModel>().ReverseMap();
            CreateMap<User, UserCreateViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordUtil.HashPassword(src.Password)));
        }
    }
}
