using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Owin;

namespace IdentityHostRequester
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44333",
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "LeagueComparer" }
            });

            var apiConfiguration = new HttpConfiguration();

            apiConfiguration.MapHttpAttributeRoutes();

            apiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            apiConfiguration.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(apiConfiguration);
        }
    }
}