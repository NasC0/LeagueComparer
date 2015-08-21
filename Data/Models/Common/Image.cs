using Newtonsoft.Json;
namespace Models.Common
{
    public class Image
    {
        [JsonProperty("w")]
        public int Width { get; set; }
        [JsonProperty("full")]
        public string Name { get; set; }
        public string Sprite { get; set; }
        public string Group { get; set; }
        [JsonProperty("h")]
        public int Height { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
    }
}
