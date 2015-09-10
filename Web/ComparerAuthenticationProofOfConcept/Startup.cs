using System;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using ComparerAuthenticationProofOfConcept.Infrastructure;
using ComparerAuthenticationProofOfConcept.OAuthServerProvider;
using Data;
using Data.Contracts;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Models;
using MongoDB.Driver;
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
            var kernel = CreateKernel();
            ConfigureAuth(app, kernel);
            ConfigureWebApi(app, kernel);

            SeedMongoDatabase.Seed(kernel.Get<IRepositoryFactory>());
        }

        private void ConfigureWebApi(IAppBuilder app, IKernel kernel)
        {
            var apiConfig = new HttpConfiguration();
            apiConfig.DependencyResolver = new NinjectResolver(kernel);
            apiConfig.MapHttpAttributeRoutes();

            apiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            
            apiConfig.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            app.UseWebApi(apiConfig);
        }

        private void ConfigureAuth(IAppBuilder app, IKernel kernel)
        {
            app.CreatePerOwinContext(() => kernel.Get<IRepositoryFactory>());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(kernel),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = false,
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseOAuthBearerTokens(oAuthOptions);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
