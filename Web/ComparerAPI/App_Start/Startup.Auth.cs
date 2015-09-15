using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ComparerAPI.Providers;
using ComparerAPI.Models;
using Data.Contracts;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using Ninject;

namespace ComparerAPI
{
    public partial class Startup
    {
        private const string FacebookGraphApi = "https://graph.facebook.com";
        private const string FacebookGraphEmailRequest = "me?fields=name,email";

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app, IKernel kernel)
        {
            app.CreatePerOwinContext(() => kernel.Get<IRepositoryFactory>());
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //// In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            facebookOptions = new FacebookAuthenticationOptions()
            {
                AppId = "1656794197865890",
                AppSecret = "276744e85601ad23053648065c5eaf19",
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
            };
            facebookOptions.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = async context =>
                {
                    context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
                }
            };

            app.UseFacebookAuthentication(facebookOptions);

            googleOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "793101710745-gko0r0entbg6vde240m95eit3u35cc6p.apps.googleusercontent.com",
                ClientSecret = "8-f77U7KPcPZ25dum2H02JIu",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
                    }
                }
            };

            app.UseGoogleAuthentication(googleOptions);
        }
    }
}
