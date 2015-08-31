using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Models.Common;
using Models.Common.Champion;
using Models.Common.Enumerations;
using Models.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Models
{
    [Serializable]
    public class Champion : Entity
    {
        public IEnumerable<String> Tags { get; set; }

        [JsonProperty("id")]
        [BsonElement("id")]
        public int ChampionId { get; set; }
        public ChampionStats Stats { get; set; }
        public string Name { get; set; }
        public PassiveSpell Passive { get; set; }
        public Image Image { get; set; }
        public IEnumerable<Spell> Spells { get; set; }
        [JsonConverter(typeof(GenericEnumConverter<ChampionResourceType>))]
        public ChampionResourceType Partype { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return this.Name + ' ' + this.Title;
        }

        public override bool Equals(object obj)
        {
            var objAsChamp = obj as Champion;
            
            if (objAsChamp == null)
            {
                return false;
            }

            bool areTagsEqual = CollectionEquality.CheckForEquality<string, string>(this.Tags, objAsChamp.Tags, x => x);
            bool areIDsEqual = this.ChampionId == objAsChamp.ChampionId;
            bool areStatsEqual = this.Stats.Equals(objAsChamp.Stats);
            bool areNamesEqual = this.Name == objAsChamp.Name;
            bool arePassivesEqual = this.Passive.Equals(objAsChamp.Passive);
            bool areImagesEqual = this.Image.Equals(objAsChamp.Image);
            bool areSpellsEqual = CollectionEquality.CheckForEquality<Spell, string>(this.Spells, objAsChamp.Spells, x => x.Key);
            bool arePartypesEqual = this.Partype == objAsChamp.Partype;
            bool areTitlesEqual = this.Title == objAsChamp.Title;

            if (areTagsEqual && areIDsEqual && areStatsEqual && areNamesEqual &&
                arePassivesEqual && areImagesEqual && areSpellsEqual && arePartypesEqual
                && areTitlesEqual)
            {
                return true;
            }

            return false;
        }
    }
}
