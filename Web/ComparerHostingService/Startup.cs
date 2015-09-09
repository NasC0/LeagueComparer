using System.Net.Http.Headers;
using System.Web.Http;
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
    }
}
