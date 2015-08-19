using System.Collections.Generic;
using Models.Common;

namespace Models
{
    public class Item : Entity
    {
        public IEnumerable<string> Tags { get; set; }
        public int ItemId { get; set; }
        public string SanitizedDescription { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public Gold Gold { get; set; }
        public IEnumerable<int> From { get; set; }
        public string Group { get; set; }
        public int Depth { get; set; }
    }
}
