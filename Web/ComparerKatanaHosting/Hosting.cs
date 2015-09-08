using System;
using Microsoft.Owin.Hosting;

namespace ComparerKatanaHosting
{
    public class Hosting
    {
        static void Main()
        {
            string uri = "http://localhost:8080";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopping!");
            }
        }
    }
}
