namespace Models.Common.Champion
{
    public class ChampionStats
    {
        public double AttackRange { get; set; }
        public double MpPerLevel { get; set; }
        public double Mp { get; set; }
        public double AttackDamage { get; set; }
        public double Hp { get; set; }
        public double HpPerLevel { get; set; }
        public double AttackDamagePerLevel { get; set; }
        public double Armor { get; set; }
        public double MpRegenPerLevel { get; set; }
        public double HpRegen { get; set; }
        public double CritPerLevel { get; set; }
        public double SpellBlockPerLevel { get; set; }
        public double MpRegen { get; set; }
        public double AttackSpeedPerLevel { get; set; }
        public double SpellBlock { get; set; }
        public double MoveSpeed { get; set; }
        public double AttackSpeedOffset { get; set; }
        public double Crit { get; set; }
        public double HpRegenPerLevel { get; set; }
        public double ArmorPerLevel { get; set; }

        public override bool Equals(object obj)
        {
            var objAsChampionStats = obj as ChampionStats;
            if (objAsChampionStats == null)
            {
                return false;
            }

            bool isAttackRangeEqual = this.AttackRange == objAsChampionStats.AttackRange;
            bool isMpPerLevelEqual = this.MpPerLevel == objAsChampionStats.MpPerLevel;
            bool isAttackDamageEqual = this.AttackDamage == objAsChampionStats.AttackDamage;
            bool isMpEqual = this.Mp == objAsChampionStats.Mp;
            bool isHpEqual = this.Hp == objAsChampionStats.Hp;
            bool isHpPerLevelEqual = this.HpPerLevel == objAsChampionStats.HpPerLevel;
            bool isAttackDamagePerLevelEqual = this.AttackDamagePerLevel == objAsChampionStats.AttackDamagePerLevel;
            bool isArmorEqual = this.Armor == objAsChampionStats.Armor;
            bool isMpRegenPerLevelEqual = this.MpRegenPerLevel == objAsChampionStats.MpRegenPerLevel;
            bool isHpRegenEqual = this.HpRegen == objAsChampionStats.HpRegen;
            bool isCritPerLevelEqual = this.CritPerLevel == objAsChampionStats.CritPerLevel;
            bool isSpellBlockPerLevelEqual = this.SpellBlockPerLevel == objAsChampionStats.SpellBlockPerLevel;
            bool isMpRegenEqual = this.MpRegen == objAsChampionStats.MpRegen;
            bool isAttackSpeedPerLevelEqual = this.AttackSpeedPerLevel == objAsChampionStats.AttackSpeedPerLevel;
            bool isSpellBlockEqual = this.SpellBlock == objAsChampionStats.SpellBlock;
            bool isMoveSpeedEqual = this.MoveSpeed == objAsChampionStats.MoveSpeed;
            bool isAttackSpeedOffsetEqual = this.AttackSpeedOffset == objAsChampionStats.AttackSpeedOffset;
            bool isCritEqual = this.Crit == objAsChampionStats.Crit;
            bool isHpRegenPerLevelEqual = this.HpRegenPerLevel == objAsChampionStats.HpRegenPerLevel;
            bool isArmorPerLevelEqual = this.ArmorPerLevel == objAsChampionStats.ArmorPerLevel;

            if (isAttackRangeEqual && isMpPerLevelEqual && isAttackDamageEqual && isMpEqual &&
                isHpEqual && isHpPerLevelEqual && isAttackDamagePerLevelEqual && isArmorEqual &&
                isMpRegenPerLevelEqual && isHpRegenEqual && isCritPerLevelEqual && isSpellBlockPerLevelEqual &&
                isMpRegenEqual && isAttackSpeedPerLevelEqual && isSpellBlockEqual && isMoveSpeedEqual &&
                isAttackSpeedOffsetEqual && isCritEqual && isHpRegenEqual && isHpRegenPerLevelEqual && isArmorPerLevelEqual)
            {
                return true;
            }

            return false;
        }
    }
}
