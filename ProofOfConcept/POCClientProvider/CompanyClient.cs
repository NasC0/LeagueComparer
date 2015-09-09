using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Models;
using Newtonsoft.Json;

namespace POCClientProvider
{
    public class CompanyClient
    {
        private string _accessToken;
        private Uri _baseRequestUri;

        public CompanyClient(Uri baseUri, string accessToken)
        {
            this._accessToken = accessToken;
            this._baseRequestUri = baseUri;
        }

        void SetClientAuthentication(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                response = await client.GetAsync(this._baseRequestUri);
            }

            var companiesString = await response.Content.ReadAsStringAsync();
            var companies = JsonConvert.DeserializeObject<IEnumerable<Company>>(companiesString);
            return companies;
        }

        public async Task<Company> GetCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);

                response = await client.GetAsync(new Uri(this._baseRequestUri, id.ToString()));
            }

            var companyString = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(companyString);
            return company;
        }

        public async Task<HttpStatusCode> AddCompanyAsync(Company company)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                string postBody = JsonConvert.SerializeObject(company);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(this._baseRequestUri, new StringContent(postBody, Encoding.UTF8, "application/json"));
            }

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UpdateCompanyAsync(Company company)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);
                string putBody = JsonConvert.SerializeObject(company);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = await client.PostAsync(this._baseRequestUri, new StringContent(putBody, Encoding.UTF8, "application/json"));
            }

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteCompanyAsync(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                SetClientAuthentication(client);

                var requestUrl = this._baseRequestUri + "/" + id.ToString();
                response = await client.DeleteAsync(requestUrl);
            }

            return response.StatusCode;
        }
    }
}
