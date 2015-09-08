using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace TokenRequester
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenResponse = GetClientToken();
            CallApi(tokenResponse);
        }

        public static TokenResponse GetClientToken()
        {
            var client = new TokenClient("https://localhost:44333/connect/token", "carbon",
                "21B5F798-BE55-42BC-8AA8-0025B903DC3B");

            //return client.RequestClientCredentialsAsync("LeagueComparer").Result;
            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "LeagueComparer").Result;
        }

        public static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);
            Console.WriteLine(client.GetStringAsync("http://localhost:60742/api/test").Result);
        }
    }
}
