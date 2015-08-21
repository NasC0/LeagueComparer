using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Common;
using Models.Common.Enumerations;
using Newtonsoft.Json;
using System.Linq;

namespace ModelsTests
{
    [TestClass]
    public class StatsConverterTests
    {
        [TestMethod]
        public void Converting_ValidParameters_ExpectValidResult()
        {
            // ARRANGE
            string statsJson = @"{
   ""stats"": {""FlatMPPoolMod"": 200},
}";
            Stat expectedStat = new Stat()
            {
                Modifies = Modifies.MPPool,
                ModifierApplicationRules = ModifierApplicationRules.Mod,
                ModifyType = ModifyType.Flat,
                Value = 200.0
            };

            // ACT
            var resultingStat = JsonConvert.DeserializeObject<Item>(statsJson);

            // ASSERT
            foreach (var stat in resultingStat.Stats)
            {
                Assert.AreEqual(expectedStat, stat);
            }
        }

        [TestMethod]
        public void Converting_MultipleStatParameters_ExpectValidResult()
        {
            // ARRANGE
            string statsJson = @"{
   ""stats"": {
				""FlatPhysicalDamageMod"": 20.0,
				""PercentLifeStealMod"": 0.1,
				""rPercentCooldownMod"": 5,
				""rFlatMPModPerLevel"": 4
			}
}";
            List<Stat> expectedStats = new List<Stat>
            {
                new Stat()
                {
                    Modifies = Modifies.PhysicalDamage,
                    ModifierApplicationRules = ModifierApplicationRules.Mod,
                    ModifyType = ModifyType.Flat,
                    Value = 20.0
                },
                new Stat()
                {
                    Modifies = Modifies.LifeSteal,
                    ModifierApplicationRules = ModifierApplicationRules.Mod,
                    ModifyType = ModifyType.Percent,
                    Value = 0.1
                },
                new Stat()
                {
                    Modifies = Modifies.Cooldown,
                    ModifierApplicationRules = ModifierApplicationRules.Mod,
                    ModifyType = ModifyType.Percent,
                    Value = 5
                },
                new Stat()
                {
                    Modifies = Modifies.MPPool,
                    ModifierApplicationRules = ModifierApplicationRules.ModPerLevel,
                    ModifyType = ModifyType.Flat,
                    Value = 4
                }
            }
                .OrderBy(x => x.Modifies)
                .ToList();

            // ACT
            var resultingItem = JsonConvert.DeserializeObject<Item>(statsJson);

            // ASSERT
            var statsAsList = resultingItem.Stats.OrderBy(x => x.Modifies).ToList();
            for (int i = 0; i < statsAsList.Count; i++)
            {
                Assert.AreEqual(expectedStats[i], statsAsList[i]);
            }
        }

        [TestMethod]
        public void Converting_MultipleStateParametersWithBorderCases_ExpectValidResult()
        {
            // ARRANGE
            string statsJson = @"{
   ""stats"": {
				""rFlatMPModPerLevel"": 4,
				""rFlatHPModPerLevel"": 4
			}
}";
            var expectedResult = new List<Stat>
            {
                new Stat()
                {
                    Modifies = Modifies.HPPool,
                    ModifierApplicationRules = ModifierApplicationRules.ModPerLevel,
                    ModifyType = ModifyType.Flat,
                    Value = 4
                },
                new Stat()
                {
                    Modifies = Modifies.MPPool,
                    ModifierApplicationRules = ModifierApplicationRules.ModPerLevel,
                    ModifyType = ModifyType.Flat,
                    Value = 4
                }
            };
            expectedResult = expectedResult.OrderBy(x => x.Modifies.ToString()).ToList();

            // ACT
            var itemResult = JsonConvert.DeserializeObject<Item>(statsJson);

            // ASSERT
            var statsAsList = itemResult.Stats.OrderBy(x => x.Modifies.ToString()).ToList();
            for (int i = 0; i < statsAsList.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], statsAsList[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Converting_StatsModifierMissingFromLists_ExpectException()
        {
            // ARRANGE
            string statsJson = @"{
   ""stats"": {
				""rFlatMPModPerLevel"": 4,
                ""rFlatMOPModPerLevel"": 4
			}
}";

            // ACT
            var itemResult = JsonConvert.DeserializeObject<Item>(statsJson);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Converting_ModifierApplicationRulesMissingFromLists_ExpectException()
        {
            // ARRANGE
            string statsJson = @"{
   ""stats"": {
				""rFlatMPModPerLevel"": 4,
                ""rFlatMPPoolMoodPerLevvel"": 4
			}
}";

            // ACT
            var itemResult = JsonConvert.DeserializeObject<Item>(statsJson);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Converting_ModifyTypeMissingFromLists_ExpectException()
        {
            // ARRANGE
            string statsJson = @"{
   ""stats"": {
				""rFlatMPModPerLevel"": 4,
                ""rFALtMPPoolModPerLevel"": 4
			}
}";

            // ACT
            var itemResult = JsonConvert.DeserializeObject<Item>(statsJson);
        }
    }
}
