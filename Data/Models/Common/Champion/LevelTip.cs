using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common.Champion
{
    public class LevelTip
    {
        public IEnumerable<string> Effect { get; set; }
        public IEnumerable<string> Label { get; set; }
    }
}
