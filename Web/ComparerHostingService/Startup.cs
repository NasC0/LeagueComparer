using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using ComparerHostingService.OwinMiddleware.BasicAuthentication;
using ComparerHostingService.Security;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Owin;

namespace ComparerHostingService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureWebApi(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            app.Map("/identity", id =>
            {
                id.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    SigningCertificate = LoadCertificate(),
                    Factory = new IdentityServerServiceFactory()
                        .UseInMemoryUsers(Users.Get())
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryScopes(StandardScopes.All)
                });
            });

            var apiConfig = new HttpConfiguration();

            apiConfig.MapHttpAttributeRoutes();
            apiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            apiConfig.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            app.UseWebApi(apiConfig);
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
