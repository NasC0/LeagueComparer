using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace POCClientProvider
{
    public class ApiClientProvider
    {
        private string _hostUri;

        public ApiClientProvider(string hostUri)
        {
            this._hostUri = hostUri;
        }

        public string AccessToken { get; private set; }

        public async Task<IDictionary<string, string>> GetTokenDictionary(string username, string password)
        {
            HttpResponseMessage response;

            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            };

            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var tokenEndpoint = new Uri(new Uri(this._hostUri), "Token");
                response = await client.PostAsync(tokenEndpoint, content);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("Error: {0}", responseContent));
            }

            return this.GetTokenDictionary(responseContent);
        }

        private IDictionary<string, string> GetTokenDictionary(string responseContent)
        {
            var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
            return tokenDictionary;
        }
    }
}
