using System.Web.Http.Dependencies;
using Ninject;

namespace ComparerAuthenticationProofOfConcept.Infrastructure
{
    public class NinjectResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel);
        }
    }
}
