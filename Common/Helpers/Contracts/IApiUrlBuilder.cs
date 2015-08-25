using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;

namespace Helpers.Contracts
{
    public interface IApiUrlBuilder
    {
        string BuildApiStaticDataUrl(Regions region);
    }
}
