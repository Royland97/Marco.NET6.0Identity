using Core.Domain.Users;

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
            CreateMap<User, UserModel>();
            CreateMap<User, UserModelList>();

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
