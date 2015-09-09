using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Serilog;

namespace ComparerAuthenticationProofOfConcept
{
    public class EntryPoint
    {
        private static IDisposable _server;

        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(
                    outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            var startupOptions = new StartOptions();
            startupOptions.Urls.Add("http://localhost:8081");
            using (WebApp.Start<Startup>(startupOptions))
            {
                Console.WriteLine("Starting WebApi");
                Console.WriteLine("Press enter to stop");
                Console.ReadLine();
                Console.WriteLine("Stopping...");
            }
        }
    }
}
