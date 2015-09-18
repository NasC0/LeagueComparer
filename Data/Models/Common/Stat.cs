using ApiProcessing.Models.JsonConverters;
using Models.Common.Enumerations;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace Models.Common
{
    public class Stat
    {
        [JsonConverter(typeof(GenericEnumConverter<Modifies>))]
        public Modifies Modifies { get; set; }
        [JsonConverter(typeof(GenericEnumConverter<ModifierApplicationRules>))]
        public ModifierApplicationRules ModifierApplicationRules { get; set; }
        [JsonConverter(typeof(GenericEnumConverter<ModifyType>))]
        public ModifyType ModifyType { get; set; }
        public double Value { get; set; }

        public override bool Equals(object obj)
        {
            var objAsStat = obj as Stat;
            if (objAsStat == null)
            {
                return false;
            }

            if (this.Modifies == objAsStat.Modifies &&
                this.ModifierApplicationRules == objAsStat.ModifierApplicationRules &&
                this.ModifyType == objAsStat.ModifyType &&
                this.Value == objAsStat.Value)
            {
                return true;
            }

            return false;
        }
    }
}
