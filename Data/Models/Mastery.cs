using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
