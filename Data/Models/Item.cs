using System.Collections.Generic;
using ApiProcessing.Models.JsonConverters;
using Models.Common;
using Newtonsoft.Json;
using System.Linq;
using Helpers;
using System;

namespace Models
{
    public class Item : Entity
    {
        public Item()
        {
            this.Available = true;
        }

        public IEnumerable<string> Tags { get; set; }

        [JsonProperty("id")]
        public int ItemId { get; set; }
        public string SanitizedDescription { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public Gold Gold { get; set; }
        public IEnumerable<int> From { get; set; }
        public IEnumerable<int> Into { get; set; }
        [JsonConverter(typeof(StatsJsonConverter))]
        public IEnumerable<Stat> Stats { get; set; }
        public string Group { get; set; }
        public int Depth { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            var objAsItem = obj as Item;
            if (objAsItem == null)
            {
                return false;
            }

            bool areTagsEqual = CollectionEquality.CheckForEquality<string, string>(this.Tags, objAsItem.Tags, x => x);
            bool areIdsEqual = this.ItemId == objAsItem.ItemId;
            bool areSanitizedDescriptionsEqual = this.SanitizedDescription == objAsItem.SanitizedDescription;
            bool areDescriptionsEqual = this.Description == objAsItem.Description;
            bool areNamesEqual = this.Name == objAsItem.Name;
            bool areImagesEqual = this.Image.Equals(objAsItem.Image);

            bool isFromEqual = CollectionEquality.CheckForEquality<int, int>(this.From, objAsItem.From, x => x);
            bool isIntoEqqual = CollectionEquality.CheckForEquality<int, int>(this.Into, objAsItem.Into, x => x);
            bool areStatsEqual = CollectionEquality.CheckForEquality<Stat, double>(this.Stats, objAsItem.Stats, x => x.Value);
            bool isGroupEqual = this.Group == objAsItem.Group;
            bool isDepthEqual = this.Depth == objAsItem.Depth;

            if (areTagsEqual && areIdsEqual && areSanitizedDescriptionsEqual && areDescriptionsEqual && areNamesEqual &&
                areImagesEqual && isFromEqual && isIntoEqqual && areStatsEqual && isGroupEqual && isDepthEqual)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + this.ItemId.GetHashCode();
            hash = hash * 23 + this.Gold.Total.GetHashCode();
            hash = hash * 23 + this.Depth.GetHashCode();
            hash = hash * 23 + this.Name.GetHashCode();

            return hash;
        }
    }
}
