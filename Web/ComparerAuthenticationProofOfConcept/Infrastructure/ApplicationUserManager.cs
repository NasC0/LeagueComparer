using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Models;
using Ninject;

namespace ComparerAuthenticationProofOfConcept.Infrastructure
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var repoFactory = context.Get<IRepositoryFactory>();
            var userStore = new MongoUserStore<User>(repoFactory);

            var applicationUserManager = new ApplicationUserManager(userStore);
            return applicationUserManager;
        }
    }
}
