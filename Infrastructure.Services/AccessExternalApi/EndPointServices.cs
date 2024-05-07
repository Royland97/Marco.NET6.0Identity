using System.Net.Http.Headers;

namespace Infrastructure.Services.AccessExternalApi
{
    /// <summary>
    /// Define an HTTP methods using JWT Authentication
    /// </summary>
    public class EndPointServices: IEndPointServices
    {
        private readonly ITokenServices _tokenServices;
        public EndPointServices(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        /// <summary>
        /// Gets the content of an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        public virtual async Task<string> GetContent(string url, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpClientHandler handler = new();
            HttpClient client = new(handler);

            var token = await _tokenServices.GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(cancellationToken);

                handler.Dispose();
                client.Dispose();

                return json;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Saves a Content to an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="dictionary">A dictionary with the Content</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        public virtual async Task<string> SaveContent(string url, Dictionary<string, string> dictionary, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpClientHandler handler = new();
            HttpClient client = new(handler);

            var token = await _tokenServices.GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new FormUrlEncodedContent(dictionary);

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content, cancellationToken);

                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                handler.Dispose();
                client.Dispose();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update a Content to an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="dictionary">A dictionary with the Content</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        public virtual async Task<string> UpdateContent(string url, Dictionary<string, string> dictionary, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpClientHandler handler = new();
            HttpClient client = new(handler);

            var token = await _tokenServices.GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new FormUrlEncodedContent(dictionary);

            try
            {
                HttpResponseMessage response = await client.PutAsync(url, content, cancellationToken);

                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                handler.Dispose();
                client.Dispose();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a Content to an url
        /// </summary>
        /// <param name="url">The url address</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw an exceptions with the HttpRequest Message</exception>
        public virtual async Task<string> DeleteContent(string url, CancellationToken cancellationToken)
        {
            HttpClientHandler handler = new();
            HttpClient client = new(handler);

            var token = await _tokenServices.GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url, cancellationToken);

                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                handler.Dispose();
                client.Dispose();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
