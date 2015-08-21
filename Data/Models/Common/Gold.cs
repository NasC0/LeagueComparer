
namespace Models.Common
{
    /// <summary>
    /// Name should be Price, but is named Gold since that's the object name in the League of Legends official API
    /// </summary>
    public class Gold
    {
        public int Total { get; set; }
        public bool Purchasable { get; set; }
        public int Sell { get; set; }
        public int Base { get; set; }
    }
}
