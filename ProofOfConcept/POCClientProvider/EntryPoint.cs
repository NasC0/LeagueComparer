using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComparerAuthenticationProofOfConcept.Models;

namespace POCClientProvider
{
    public class EntryPoint
    {
        public static void Main()
        {
            Run().Wait();
        }

        public static async Task Run()
        {
            // Create an http client provider:
            string tokenUriString = "http://localhost:8081";
            string hostUriString = "http://localhost:8081/api/Company";
            var provider = new ApiClientProvider(tokenUriString);
            string _accessToken;
            IDictionary<string, string> _tokenDictionary;

            try
            {
                // Pass in the credentials and retrieve a token dictionary:
                _tokenDictionary =
                    await provider.GetTokenDictionary("john@example.com", "JohnsPassword");
                _accessToken = _tokenDictionary["access_token"];

                // Write the contents of the dictionary:
                foreach (var kvp in _tokenDictionary)
                {
                    Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("");
                }

                // Create a company client instance:
                var baseUri = new Uri(hostUriString);
                var companyClient = new CompanyClient(baseUri, _accessToken);

                // Read initial companies:
                Console.WriteLine("Read all the companies...");
                var companies = await companyClient.GetCompaniesAsync();
                WriteCompaniesList(companies);

                int nextId = (from c in companies select c.Id).Max() + 1;

                Console.WriteLine("Add a new company...");
                var result = await companyClient.AddCompanyAsync(
                    new Company { Name = string.Format("New Company #{0}", nextId) });
                WriteStatusCodeResult(result);

                Console.WriteLine("Updated List after Add:");
                companies = await companyClient.GetCompaniesAsync();
                WriteCompaniesList(companies);

                Console.WriteLine("Update a company...");
                var updateMe = await companyClient.GetCompanyAsync(nextId);
                updateMe.Name = string.Format("Updated company #{0}", updateMe.Id);
                result = await companyClient.UpdateCompanyAsync(updateMe);
                WriteStatusCodeResult(result);

                Console.WriteLine("Updated List after Update:");
                companies = await companyClient.GetCompaniesAsync();
                WriteCompaniesList(companies);

                Console.WriteLine("Delete a company...");
                result = await companyClient.DeleteCompanyAsync(nextId - 1);
                WriteStatusCodeResult(result);

                Console.WriteLine("Updated List after Delete:");
                companies = await companyClient.GetCompaniesAsync();
                WriteCompaniesList(companies);
            }
            catch (AggregateException ex)
            {
                // If it's an aggregate exception, an async error occurred:
                Console.WriteLine(ex.InnerExceptions[0].Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                // Something else happened:
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press the Enter key to Exit...");
                Console.ReadLine();
                return;
            }
        }

        static void WriteCompaniesList(IEnumerable<Company> companies)
        {
            foreach (var company in companies)
            {
                Console.WriteLine("Id: {0} Name: {1}", company.Id, company.Name);
            }
            Console.WriteLine("");
        }

        static void WriteStatusCodeResult(System.Net.HttpStatusCode statusCode)
        {
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Opreation Succeeded - status code {0}", statusCode);
            }
            else
            {
                Console.WriteLine("Opreation Failed - status code {0}", statusCode);
            }
            Console.WriteLine("");
        }
    }
}
