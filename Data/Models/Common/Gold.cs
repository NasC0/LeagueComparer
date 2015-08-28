
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

        public override bool Equals(object obj)
        {
            var objAsGold = obj as Gold;
            if (objAsGold == null)
            {
                return false;
            }

            bool isTotalEqual = this.Total == objAsGold.Total;
            bool isPurchasableEqual = this.Purchasable == objAsGold.Purchasable;
            bool isSellEqual = this.Sell == objAsGold.Sell;
            bool isBaseEqual = this.Base == objAsGold.Base;

            if (isTotalEqual && isPurchasableEqual &&
                isSellEqual && isBaseEqual)
            {
                return true;
            }

            return false;
        }
    }
}
