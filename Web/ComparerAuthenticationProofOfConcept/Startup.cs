using System;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using ComparerAuthenticationProofOfConcept.Infrastructure;
using ComparerAuthenticationProofOfConcept.OAuthServerProvider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace ComparerAuthenticationProofOfConcept
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureWebApi(app);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var apiConfig = new HttpConfiguration();
            apiConfig.DependencyResolver = new NinjectResolver(CreateKernel());
            apiConfig.MapHttpAttributeRoutes();

            apiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            
            apiConfig.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            app.UseWebApi(apiConfig);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
