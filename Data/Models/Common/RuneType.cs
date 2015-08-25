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
    }
}
