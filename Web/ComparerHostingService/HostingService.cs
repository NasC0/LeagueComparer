using System;
using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace ComparerHostingService
{
    partial class HostingService : ServiceBase
    {
        private IDisposable _hostingServer;

        public HostingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debugger.Launch();
            var startupOptions = new StartOptions();
            startupOptions.Urls.Add("https://localhost:8080");

            _hostingServer = WebApp.Start<Startup>(startupOptions);
        }

        protected override void OnStop()
        {
            if (_hostingServer != null)
            {
                _hostingServer.Dispose();
            }

            base.OnStop();
        }
    }
}
