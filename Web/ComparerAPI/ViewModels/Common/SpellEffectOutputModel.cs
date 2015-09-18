using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComparerAPI.ViewModels.Common
{
    public class SpellEffectOutputModel
    {
        public string Key { get; set; }
        public IEnumerable<double> Value { get; set; }
    }
}