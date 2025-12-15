using Infrastructure.Services.AccessExternalApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services.AccessExternalApi
{
    public class HostNameServices: IHostNameServices
    {
        public HostNameServices()
        {
        }

        /// <summary>
        /// Gets the Host Name from appsettings
        /// </summary>
        /// <returns></returns>
        public virtual async Task<string> GetHostNameAsync()
        {
            StreamReader reader = new("appsettings.json");

            var json = await reader.ReadToEndAsync();

            reader.Close();

            var tokenObject = JsonConvert.DeserializeObject<JObject>(json);

            if (tokenObject["ExternalApiHost"] == null)
                throw new Exception("Host Name not found");

            var aux = JsonConvert.DeserializeObject<HostNameModel>(tokenObject["ExternalApiHost"].ToString());

            return aux.Name;
        }
    }
}
