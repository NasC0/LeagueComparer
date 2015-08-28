using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class CollectionEquality
    {
        public static bool CheckForEquality<T, TRes>(object firstObject, object secondObject, Func<T, TRes> orderingSequence)
        {
            bool areEqual = false;
            bool isFirstObjectNull = firstObject == null;
            bool isSecondObjectNull = secondObject == null;

            if (isFirstObjectNull && isSecondObjectNull)
            {
                return true;
            }

            if (isFirstObjectNull != isSecondObjectNull)
            {
                return false;
            }

            var firstObjectCast = firstObject as IEnumerable<T>;
            var secondObjectCast = secondObject as IEnumerable<T>;
            if (firstObjectCast == null || secondObjectCast == null)
            {
                return false;
            }

            areEqual = firstObjectCast.OrderBy(orderingSequence).SequenceEqual(secondObjectCast.OrderBy(orderingSequence));

            return areEqual;
        }
    }
}
