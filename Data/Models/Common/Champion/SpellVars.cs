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

        public override bool Equals(object obj)
        {
            var objAsSpellVar = obj as SpellVars;

            if (objAsSpellVar == null)
            {
                return false;
            }

            bool isLinkEqual = this.Link == objAsSpellVar.Link;
            bool areCoefficientsEqual = this.Coeff.OrderBy(c => c).SequenceEqual(objAsSpellVar.Coeff.OrderBy(c => c));
            bool areKeysEqual = this.Key == objAsSpellVar.Key;

            if (isLinkEqual && areCoefficientsEqual && areKeysEqual)
            {
                return true;
            }

            return false;
        }
    }
}
