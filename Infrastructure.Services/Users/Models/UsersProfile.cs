using Core.Domain.Users;
using Infrastructure.Services.Users.Models.Resolvers;

namespace Infrastructure.Services.Users.Models
{
    /// <summary>
    /// User, Role and Resource Profile
    /// </summary>
    public class UsersProfile: BaseProfile
    {
        public UsersProfile()
        {
            //User
            CreateMap<UserModel, User>()
                .ForMember(dst => dst.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dst => dst.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()));
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
