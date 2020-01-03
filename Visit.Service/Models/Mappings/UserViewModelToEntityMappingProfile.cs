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
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.BirthPlace, map => map.MapFrom(vm => vm.BirthPlace));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.ResidesIn, map => map.MapFrom(vm => vm.ResidesIn));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.Education, map => map.MapFrom(vm => vm.Education));
            CreateMap<RegistrationUserApi, User>().ForMember(au => au.OccupationTitle, map => map.MapFrom(vm => vm.OccupationTitle));
            
            CreateMap<User, LoggedInUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.UserName));
            CreateMap<User, LoggedInUser>().ForMember(au => au.FName, map => map.MapFrom(vm => vm.FName));
            CreateMap<User, LoggedInUser>().ForMember(au => au.LName, map => map.MapFrom(vm => vm.LName));
            CreateMap<User, LoggedInUser>().ForMember(au => au.Email, map => map.MapFrom(vm => vm.Email));
            CreateMap<User, LoggedInUser>().ForMember(au => au.Birthday, map => map.MapFrom(vm => vm.Birthday));
            CreateMap<User, LoggedInUser>().ForMember(au => au.BirthPlace, map => map.MapFrom(vm => vm.BirthPlace));
            CreateMap<User, LoggedInUser>().ForMember(au => au.ResidesIn, map => map.MapFrom(vm => vm.ResidesIn));
            CreateMap<User, LoggedInUser>().ForMember(au => au.Education, map => map.MapFrom(vm => vm.Education));
            CreateMap<User, LoggedInUser>().ForMember(au => au.OccupationTitle, map => map.MapFrom(vm => vm.OccupationTitle));
        }

    }
}