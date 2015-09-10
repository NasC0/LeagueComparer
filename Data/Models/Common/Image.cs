using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
namespace Models.Common
{
    [Serializable]
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

        public override bool Equals(object obj)
        {
            var objAsImage = obj as Image;
            if (objAsImage == null)
            {
                return false;
            }

            bool isWidthEqual = this.Width == objAsImage.Width;
            bool isNameEqual = this.Name == objAsImage.Name;
            bool isSpriteEqual = this.Sprite == objAsImage.Sprite;
            bool isGroupEqual = this.Group == objAsImage.Group;
            bool isHeightEqual = this.Height == objAsImage.Height;
            bool isYEqual = this.Y == objAsImage.Y;
            bool isXEqual = this.X == objAsImage.X;

            if (isWidthEqual && isNameEqual && isSpriteEqual &&
                isGroupEqual && isHeightEqual && isYEqual &&
                isXEqual)
            {
                return true;
            }

            return false;
        }
    }
}
