using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;

namespace ComparerKatanaHosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureWebApi(app);

            //app.Use(async (env, next) =>
            //{
            //    foreach (var pair in env.Environment)
            //    {
            //        Console.WriteLine("{0}: {1}", pair.Key, pair.Value.ToString());
            //    }

            //    await next();
            //});


            app.Use(async (env, next) =>
            {
                Console.WriteLine("Requesting: " + env.Request.Path);
                await next();
                Console.WriteLine("Response: " + env.Response.StatusCode);
                Console.WriteLine("---------------------");
            });

            app.UseHelloWorld();
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Set JSON format as default response data format
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            app.UseWebApi(config);
        }
    }

    public static class AppBuilderUseExtensions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }
}
