using Core.Domain.Users;

namespace Infrastructure.Services.Users.Models
{
    /// <summary>
    /// User Profile
    /// </summary>
    public class UserProfile: BaseProfile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(dst => dst.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dst => dst.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()));
            CreateMap<User, UserModel>();
            CreateMap<User, UserModelList>();
        }
    }
}
