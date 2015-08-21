using System;
using System.Collections.Generic;
using Models.Common;

namespace ApiProcessing.Models.JsonConverters
{
    public interface IStatConverter
    {
        IEnumerable<Stat> ConvertStats(IEnumerable<Tuple<string, double>> statsRead);
    }
}
