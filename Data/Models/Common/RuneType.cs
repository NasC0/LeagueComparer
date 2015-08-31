using Models.Common.Enumerations;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace Models.Common
{
    public class RuneType
    {
        public bool IsRune { get; set; }
        public int Tier { get; set; }

        [JsonConverter(typeof(GenericEnumConverter<RuneTypes>))]
        public RuneTypes Type { get; set; }

        public override bool Equals(object obj)
        {
            var objAsRuneType = obj as RuneType;
            if (objAsRuneType == null)
            {
                return false;
            }

            bool isIsRunesEqual = this.IsRune == objAsRuneType.IsRune;
            bool isTierEqual = this.Tier == objAsRuneType.Tier;
            bool isTypeEqual = this.Type == objAsRuneType.Type;

            if (isIsRunesEqual && isTierEqual && isTypeEqual)
            {
                return true;
            }

            return false;
        }
    }
}
