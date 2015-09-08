using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Serilog;

namespace IdentityServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(
                    outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            using (WebApp.Start<Startup>("https://localhost:44333"))
            {
                Console.WriteLine("Server running");
                Console.ReadLine();
            }
        }
    }
}
