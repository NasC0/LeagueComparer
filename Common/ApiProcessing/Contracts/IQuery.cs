using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiProcessing.Enumerations;

namespace ApiProcessing.Contracts
{
    public interface IQuery
    {
        ObjectType ObjectType { get; }

        /// <summary>
        /// Gets the exact query string ready for execution, based on the parameters passed in the constructor
        /// </summary>
        /// <returns>Query string ready for sending</returns>
        string GetQueryString();
    }
}
