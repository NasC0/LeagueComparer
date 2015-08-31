using System;
namespace Models.Common.Champion
{
    [Serializable]
    public class PassiveSpell
    {
        public string Description { get; set; }
        public string SanitizedDescription { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }

        public override bool Equals(object obj)
        {
            var objAsPassiveSpell = obj as PassiveSpell;

            if (objAsPassiveSpell == null)
            {
                return false;
            }

            bool isDescriptionEqual = this.Description == objAsPassiveSpell.Description;
            bool isSanitizedDescriptionEqual = this.SanitizedDescription == objAsPassiveSpell.SanitizedDescription;
            bool isNameEqual = this.Name == objAsPassiveSpell.Name;
            bool isImageEqual = this.Image.Equals(objAsPassiveSpell.Image);

            if (isDescriptionEqual && isSanitizedDescriptionEqual &&
                isNameEqual && isImageEqual)
            {
                return true;
            }

            return false;
        }
    }
}
