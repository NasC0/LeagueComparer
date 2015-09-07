using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace ComparerKatanaHosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
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
            });

            app.Use<HelloWorldComponent>();
            //app.UseWelcomePage();

            //app.Run(ctx =>
            //{
            //    return ctx.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
