using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparerKatanaHosting
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class DumpRequestResponse
    {
        private AppFunc next;

        public DumpRequestResponse(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            foreach (var pair in environment)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

            await this.next(environment);
        }
    }
}
