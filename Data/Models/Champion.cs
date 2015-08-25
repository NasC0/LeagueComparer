using System;
using System.Collections.Generic;
using Models.Common;
using Models.Common.Champion;
using Models.Common.Enumerations;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace Models
{
    public class Champion : Entity
    {
        public IEnumerable<String> Tags { get; set; }
        [JsonProperty("id")]
        public int ChampionId { get; set; }
        public ChampionStats Stats { get; set; }
        public string Name { get; set; }
        public PassiveSpell Passive { get; set; }
        public Image Image { get; set; }
        public IEnumerable<Spell> Spells { get; set; }
        [JsonConverter(typeof(GenericEnumConverter<ChampionResourceType>))]
        public ChampionResourceType Partype { get; set; }
        public string Lore { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return this.Name + ' ' + this.Title;
        }
    }
}
