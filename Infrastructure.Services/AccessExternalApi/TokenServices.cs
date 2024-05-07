using Infrastructure.Services.AccessExternalApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services.AccessExternalApi
{
    /// <summary>
    /// Get the Access Token used for EndPoint Services Authentication
    /// </summary>
    public class TokenServices: ITokenServices
    {
        private readonly IHostNameServices _hostNameServices;

        public TokenServices(IHostNameServices hostNameServices)
        {
            _hostNameServices = hostNameServices;
        }

        /// <summary>
        /// Gets the AccessToken
        /// </summary>
        /// <returns></returns>
        public virtual async Task<string> GetAccessTokenAsync()
        {
            var token = await GetTokenAsync();

            if (DateTime.Compare(DateTime.Now, token.Expires) > 0 || token == null)
            {
                RefreshTokenAsync();
                GetAccessTokenAsync();
            }

            return token.Token;
        }

        /// <summary>
        /// Gets the TokenModel
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TokenModel> GetTokenAsync()
        {
            StreamReader reader = new("appsettings.json");

            var json = await reader.ReadToEndAsync();

            reader.Close();

            var tokenObject = JsonConvert.DeserializeObject<JObject>(json);

            if (tokenObject["TokenRecom"] == null)
            {
                RefreshTokenAsync();
                GetTokenAsync();
            }

            var aux = JsonConvert.DeserializeObject<TokenModel>(tokenObject["TokenRecom"].ToString());

            return aux;
        }

        /// <summary>
        /// Refresh the token if Expires
        /// </summary>
        /// <returns></returns>
        public async void RefreshTokenAsync()
        {
            var host = _hostNameServices.GetHostNameAsync();

            HttpClientHandler handler = new();
            HttpClient client = new(handler);

            var credentials = new Dictionary<string, string>
            {
                { "username", "admin" },
                { "password", "admin" }
            };

            var content = new FormUrlEncodedContent(credentials);

            try
            {
                HttpResponseMessage login = await client.PostAsync("http://" + host + "/api/Account/Login", content);
                string token = await login.Content.ReadAsStringAsync().ConfigureAwait(false);

                var tokenJObject = JsonConvert.DeserializeObject<JObject>(token);

                var tokenModel = new TokenModel
                {
                    Token = tokenJObject["token"].ToString(),
                    Expires = tokenJObject["expires"].Value<DateTime>()
                };

                StreamReader reader = new("appsettings.json");

                var json = await reader.ReadToEndAsync();

                reader.Close();

                var aux = JsonConvert.DeserializeObject<JObject>(json);

                aux["TokenModel"] = JObject.Parse(JsonConvert.SerializeObject(tokenModel));

                StreamWriter write = new("appsettings.json");

                write.Write(aux);

                write.Close();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
