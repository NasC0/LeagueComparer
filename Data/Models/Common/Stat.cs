using ApiProcessing.Models.JsonConverters;
using Models.Common.Enumerations;
using Newtonsoft.Json;

namespace Models.Common
{
    [JsonConverter(typeof(StatsJsonConverter))]
    public class Stat
    {
        public Modifies Modifies { get; set; }
        public ModifierApplicationRules ModifierApplicationRules { get; set; }
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
