using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common.Champion
{
    public class SpellVars
    {
        public string Link { get; set; }
        public IEnumerable<double> Coeff { get; set; }
        public string Key { get; set; }
    }
}
