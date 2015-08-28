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

        public override bool Equals(object obj)
        {
            var objAsLevelTip = obj as LevelTip;
            if (objAsLevelTip == null)
            {
                return false;
            }

            bool areEffectsEqual = this.Effect.OrderBy(e => e).SequenceEqual(objAsLevelTip.Effect.OrderBy(e => e));
            bool areLabelsEqual = this.Effect.OrderBy(e => e).SequenceEqual(objAsLevelTip.Effect.OrderBy(e => e));

            if (areEffectsEqual && areLabelsEqual)
            {
                return true;
            }

            return false;
        }
    }
}
