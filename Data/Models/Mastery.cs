using System.Collections.Generic;
using Helpers;
using Models.Common;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace Models
{
    public class Mastery : Entity
    {
        public int Ranks { get; set; }
        [JsonProperty("id")]
        public int MasteryId { get; set; }
        public IEnumerable<string> SanitizedDescription { get; set; }
        public IEnumerable<string> Description { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        [JsonProperty("prereq")]
        public string Prerequisite { get; set; }
        [JsonConverter(typeof(GenericEnumConverter<MasteryTypes>))]
        public MasteryTypes MasteryTree { get; set; }

        public override bool Equals(object obj)
        {
            var objAsMastery = obj as Mastery;
            if (objAsMastery == null)
            {
                return false;
            }

            bool areRanksEqual = this.Ranks == objAsMastery.Ranks;
            bool areIdsEqual = this.MasteryId == objAsMastery.MasteryId;
            bool areSanitizedDescriptionsEqual = CollectionEquality.CheckForEquality<string, string>(this.SanitizedDescription, objAsMastery.SanitizedDescription, x => x);
            bool areDescriptionsEqual = CollectionEquality.CheckForEquality<string, string>(this.Description, objAsMastery.Description, x => x);
            bool areNamesEqual = this.Name == objAsMastery.Name;
            bool areImagesEqual = this.Image.Equals(objAsMastery.Image);
            bool arePrerequisitesEqual = this.Prerequisite == objAsMastery.Prerequisite;
            bool areMasteryTreesEqual = this.MasteryTree == objAsMastery.MasteryTree;

            if (areRanksEqual && areIdsEqual && areSanitizedDescriptionsEqual && areDescriptionsEqual &&
                areNamesEqual && areImagesEqual && arePrerequisitesEqual && areMasteryTreesEqual)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + this.Ranks.GetHashCode();
            hash = hash * 23 + this.Name.GetHashCode();
            hash = hash * 23 + this.Prerequisite.GetHashCode();

            return hash;
        }
    }
}
