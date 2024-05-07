namespace Infrastructure.Services.AccessExternalApi
{
    public interface IHostNameServices
    {
        /// <summary>
        /// Gets the Host Name from appsettings
        /// </summary>
        /// <returns></returns>
        Task<string> GetHostNameAsync();
    }
}
