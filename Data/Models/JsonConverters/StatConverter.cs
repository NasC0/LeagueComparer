using System;
using System.Collections.Generic;
using Models.Common;
using Models.Common.Enumerations;

namespace ApiProcessing.Models.JsonConverters
{
    public class StatConverter : IStatConverter
    {
        private const string BorderCaseMPPool = "MPMod";
        private const string BorderCaseHPPool = "HPMod";
        private const string BorderCaseEnergy = "Energy";

        private IEnumerable<string> availableStatNames;
        private IEnumerable<string> availableModifierApplicationRules;
        private IEnumerable<string> availableModifyTypes;

        public StatConverter()
        {
            this.availableStatNames = Enum.GetNames(typeof(Modifies));
            this.availableModifierApplicationRules = Enum.GetNames(typeof(ModifierApplicationRules));
            this.availableModifyTypes = Enum.GetNames(typeof(ModifyType));
        }

        public IEnumerable<Stat> ConvertStats(IEnumerable<Tuple<string, double>> statsRead)
        {
            List<Stat> stats = new List<Stat>();

            foreach (var stat in statsRead)
            {
                Stat currentStat = new Stat();

                this.ConvertStatNames(stat.Item1, currentStat);
                this.ConvertModifierApplicationRules(stat.Item1, currentStat);
                this.ConvertModifyTypes(stat.Item1, currentStat);
                currentStat.Value = stat.Item2;

                stats.Add(currentStat);
            }

            return stats;
        }

        private void ConvertStatNames(string originStat, Stat destinationStat)
        {
            if (originStat.Contains(BorderCaseHPPool))
            {
                destinationStat.Modifies = Modifies.HPPool;
            }
            else if (originStat.Contains(BorderCaseMPPool))
            {
                destinationStat.Modifies = Modifies.MPPool;
            }
            else
            {
                bool isNameConverted = false;

                foreach (var statName in this.availableStatNames)
                {
                    if (originStat.Contains(statName))
                    {
                        Modifies modifies;
                        Enum.TryParse<Modifies>(statName, false, out modifies);
                        destinationStat.Modifies = modifies;

                        isNameConverted = true;
                        break;
                    }
                }

                if (!isNameConverted)
                {
                    if (originStat.Contains(BorderCaseEnergy))
                    {
                        destinationStat.Modifies = Modifies.EnergyPool;
                        return;
                    }

                    throw new ArgumentException("Non existent stat name in list", originStat);
                }
            }
        }

        private void ConvertModifierApplicationRules(string originStat, Stat destinationStat)
        {
            bool isApplicationRuleConverted = false;
            foreach (var applicationRule in this.availableModifierApplicationRules)
            {
                if (originStat.Contains(applicationRule))
                {
                    ModifierApplicationRules modifierApplicationRules;
                    Enum.TryParse<ModifierApplicationRules>(applicationRule, false, out modifierApplicationRules);
                    destinationStat.ModifierApplicationRules = modifierApplicationRules;

                    isApplicationRuleConverted = true;
                    break;
                }
            }

            if (!isApplicationRuleConverted)
            {
                throw new ArgumentException("Non existent modifier application rule in list", originStat);
            }
        }

        private void ConvertModifyTypes(string originStat, Stat destinationStat)
        {
            bool isModifyTypeConverted = false;

            foreach (var modifyType in this.availableModifyTypes)
            {
                if (originStat.Contains(modifyType))
                {
                    ModifyType resultModifyingType;
                    Enum.TryParse<ModifyType>(modifyType, false, out resultModifyingType);
                    destinationStat.ModifyType = resultModifyingType;

                    isModifyTypeConverted = true;
                    break;
                }
            }

            if (!isModifyTypeConverted)
            {
                throw new ArgumentException("Non existent modifier type in list", originStat);
            }
        }
    }
}
