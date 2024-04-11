using Core.Domain.Users;
using UserInterface.Web.ViewModels.Authentication;

namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// User, Role and Resource Profile
    /// </summary>
    public class UsersProfile: BaseProfile
    {
        public UsersProfile()
        {
            //User
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>()
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.PasswordHash));
            CreateMap<User, UserModelList>()
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.PasswordHash));
            CreateMap<RegisterModel, User>();

            //Role
            CreateMap<RoleModel, Role>()
                .ForMember(dst => dst.NormalizedName, opt => opt.MapFrom(src => src.Name.ToUpper()));
            CreateMap<Role, RoleModel>();
            CreateMap<Role, RoleModelList>();

            //Resource
            CreateMap<ResourceModel, Resource>();
            CreateMap<Resource, ResourceModel>();
        }
    }
}
