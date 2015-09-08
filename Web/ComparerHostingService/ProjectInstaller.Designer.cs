namespace ComparerHostingService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HostingServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.HostingService = new System.ServiceProcess.ServiceInstaller();
            // 
            // HostingServiceProcessInstaller
            // 
            this.HostingServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.HostingServiceProcessInstaller.Password = null;
            this.HostingServiceProcessInstaller.Username = null;
            // 
            // HostingService
            // 
            this.HostingService.ServiceName = "HostingService";
            this.HostingService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.HostingService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.HostingService_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.HostingServiceProcessInstaller,
            this.HostingService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller HostingServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller HostingService;
    }
}