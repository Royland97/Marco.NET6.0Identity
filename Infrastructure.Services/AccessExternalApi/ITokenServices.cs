using Infrastructure.Services.AccessExternalApi.Model;

namespace Infrastructure.Services.AccessExternalApi
{
    public interface ITokenServices
    {
        /// <summary>
        /// Gets the AccessToken
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync();

        /// <summary>
        /// Gets the TokenModel
        /// </summary>
        /// <returns></returns>
        Task<TokenModel> GetTokenAsync();
    }
}
