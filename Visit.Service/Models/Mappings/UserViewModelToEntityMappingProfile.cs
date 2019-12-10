using AutoMapper;

namespace visitsvc.Models.Mappings
{
    public class UserViewModelToEntityMappingProfile : Profile
    {
        public UserViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.UserName));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.FName, map => map.MapFrom(vm => vm.FName));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.LName, map => map.MapFrom(vm => vm.LName));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.Email, map => map.MapFrom(vm => vm.Email));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.Birthday, map => map.MapFrom(vm => vm.Birthday));
        }
    }
}