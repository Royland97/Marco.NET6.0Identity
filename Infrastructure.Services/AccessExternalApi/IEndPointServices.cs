namespace Infrastructure.Services.AccessExternalApi
{
    public interface IEndPointServices
    {
        /// <summary>
        /// Gets the content of an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        Task<string> GetContent(string url, CancellationToken cancellationToken);

        /// <summary>
        /// Saves a Content to an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="dictionary">A dictionary with the Content</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        Task<string> SaveContent(string url, Dictionary<string, string> dictionary, CancellationToken cancellationToken);

        /// <summary>
        /// Update a Content to an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="dictionary">A dictionary with the Content</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        Task<string> UpdateContent(string url, Dictionary<string, string> dictionary, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a Content to an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        Task<string> DeleteContent(string url, CancellationToken cancellationToken);
    }
}
