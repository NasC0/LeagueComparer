using System.Collections.Generic;
using ApiProcessing.Models.JsonConverters;
using Helpers;
using Models.Common;
using Newtonsoft.Json;

namespace Models
{
    public class Rune : Entity
    {
        public IEnumerable<string> Tags { get; set; }
        [JsonProperty("id")]
        public int RuneId { get; set; }
        public string SanitizedDescription { get; set; }
        [JsonConverter(typeof(StatsJsonConverter))]
        public IEnumerable<Stat> Stats { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        [JsonProperty("rune")]
        public RuneType RuneType { get; set; }

        public override bool Equals(object obj)
        {
            var objAsRune = obj as Rune;

            if (objAsRune == null)
            {
                return false;
            }

            bool areTagsEqual = CollectionEquality.CheckForEquality<string, string>(this.Tags, objAsRune.Tags, t => t);
            bool areIdsEqual = this.RuneId == objAsRune.RuneId;
            bool areSanitizedDescriptionsEqual = this.SanitizedDescription == objAsRune.SanitizedDescription;
            bool areStatsEqual = CollectionEquality.CheckForEquality<Stat, double>(this.Stats, objAsRune.Stats, s => s.Value);
            bool areDescriptionsEqual = this.Description == objAsRune.Description;
            bool areNamesEqual = this.Name == objAsRune.Name;
            bool areImagesEqual = this.Image.Equals(objAsRune.Image);
            bool areRuneTypesEqual = this.RuneType.Equals(objAsRune.RuneType);

            if (areTagsEqual && areIdsEqual && areSanitizedDescriptionsEqual && areStatsEqual &&
                areDescriptionsEqual && areNamesEqual && areImagesEqual && areRuneTypesEqual)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + this.RuneId.GetHashCode();
            hash = hash * 23 + this.Name.GetHashCode();
            hash = hash * 23 + this.Description.GetHashCode();
            hash = hash * 23 + this.SanitizedDescription.GetHashCode();

            return hash;
        }
    }
}
