using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerKatanaHosting
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class HelloWorldComponent
    {
        private AppFunc next;

        public HelloWorldComponent(AppFunc next)
        {
            this.next = next;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            var responseStream = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(responseStream))
            {
                return writer.WriteAsync("Hello!!");
            }

            this.next(environment);
        }
    }
}
