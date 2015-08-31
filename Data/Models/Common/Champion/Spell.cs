using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models.Common.Champion
{
    [Serializable]
    public class Spell
    {
        private IEnumerable<object> effectOriginal;

        public dynamic Range { get; set; }
        public LevelTip LevelTip { get; set; }
        public string Resource { get; set; }
        public int MaxRank { get; set; }
        public IEnumerable<string> EffectBurn { get; set; }
        public Image Image { get; set; }
        public IEnumerable<double> Cooldown { get; set; }
        public IEnumerable<int> Cost { get; set; }
        public IEnumerable<SpellVars> Vars { get; set; }
        public string SanitizedDescription { get; set; }
        public string RangeBurn { get; set; }
        public string CostType { get; set; }

        [JsonProperty("effect")]
        [BsonElement("effect")]
        public IEnumerable<object> EffectOriginal
        {
            get
            {
                return this.effectOriginal;
            }

            set
            {
                this.effectOriginal = value;
                this.SetEffect(value);
            }
        }

        [JsonIgnore]
        [BsonIgnore]
        public IEnumerable<SpellEffect> Effect { get; set; }
        public string CooldownBurn { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string SanitizedTooltip { get; set; }
        public string Key { get; set; }
        public string CostBurn { get; set; }
        public string Tooltip { get; set; }

        public void SetEffect(IEnumerable<object> effects)
        {
            if (effects == null)
            {
                this.Effect = null;
            }
            else
            {
                List<SpellEffect> spellEffects = new List<SpellEffect>();
                int spellEffectsCounter = 0;

                foreach (var effect in effects)
                {
                    string key = "e" + spellEffectsCounter;
                    var currentSpellEffect = new SpellEffect
                    {
                        Key = key
                    };

                    if (effect == null)
                    {
                        currentSpellEffect.Value = null;
                    }
                    else
                    {
                        var jsonParsed = JArray.FromObject(effect);
                        var valuesList = jsonParsed.Values<double>();

                        currentSpellEffect.Value = valuesList.ToList();
                    }

                    spellEffects.Add(currentSpellEffect);
                    spellEffectsCounter++;
                }

                this.Effect = spellEffects;
            }
        }

        public override bool Equals(object obj)
        {
            var objAsSpell = obj as Spell;

            if (objAsSpell == null)
            {
                return false;
            }

            bool areRangesEqual = false;
            bool areRangesArrays = (this.Range is int[] && objAsSpell.Range is int[]);
            bool areRangesStrings = (this.Range is string && objAsSpell.Range is string);

            if (areRangesArrays)
            {
                var thisRangeAsArray = this.Range as int[];
                var objRangeAsArray = objAsSpell.Range as int[];
                areRangesEqual = thisRangeAsArray.OrderBy(r => r).SequenceEqual(objRangeAsArray.OrderBy(r => r));
            }

            if (areRangesStrings)
            {
                var thisRangeAsString = this.Range as string;
                var objRangeAsString = objAsSpell.Range as string;
                areRangesEqual = thisRangeAsString == objRangeAsString;
            }

            bool areLevelTipsEqual = this.LevelTip.Equals(objAsSpell.LevelTip);
            bool areResourcesEqual = this.Resource == objAsSpell.Resource;
            bool areMaxRanksEqual = this.MaxRank == objAsSpell.MaxRank;
            bool areEffectsBurnEqual = (this.EffectBurn == null && objAsSpell.EffectBurn == null) || this.EffectBurn.OrderBy(e => e).SequenceEqual(objAsSpell.EffectBurn.OrderBy(e => e));
            bool areImagesEqual = this.Image.Equals(objAsSpell.Image);
            bool areCooldownsEqual = (this.Cooldown == null && objAsSpell.Cooldown == null) || this.Cooldown.OrderBy(c => c).SequenceEqual(objAsSpell.Cooldown.OrderBy(c => c));
            bool areCostsEqual = (this.Cost == null && objAsSpell.Cost == null) || this.Cost.OrderBy(c => c).SequenceEqual(objAsSpell.Cost.OrderBy(c => c));
            bool areVarsEqual = (this.Vars == null && objAsSpell.Vars == null) || this.Vars.OrderBy(v => v.Key).SequenceEqual(objAsSpell.Vars.OrderBy(v => v.Key));
            bool areSanitizedDescriptionsEqual = this.SanitizedDescription == objAsSpell.SanitizedDescription;
            bool areRangeBurnsEqual = this.RangeBurn == objAsSpell.RangeBurn;
            bool areCostTypesEqual = this.CostType == objAsSpell.CostType;
            bool areEffectsEqual = (this.Effect == null && objAsSpell.Effect == null) || this.Effect.OrderBy(e => e).SequenceEqual(objAsSpell.Effect.OrderBy(e => e));
            bool areCooldownBurnsEqual = this.CooldownBurn == objAsSpell.CooldownBurn;
            bool areDescriptionsEqual = this.Description == objAsSpell.Description;
            bool areNamesEqual = this.Name == objAsSpell.Name;
            bool areSanitizedTooltipsEqual = this.SanitizedTooltip == objAsSpell.SanitizedTooltip;
            bool areKeysEqual = this.Key == objAsSpell.Key;
            bool areCostBurnsEqual = this.CostBurn == objAsSpell.CostBurn;
            bool areTooltipsEqual = this.Tooltip == objAsSpell.Tooltip;

            if (areRangesEqual && areLevelTipsEqual && areResourcesEqual && areMaxRanksEqual &&
                areEffectsBurnEqual && areImagesEqual && areCooldownsEqual && areCostsEqual &&
                areVarsEqual && areSanitizedDescriptionsEqual && areRangeBurnsEqual && areCostTypesEqual &&
                areEffectsEqual && areCooldownBurnsEqual && areDescriptionsEqual && areNamesEqual &&
                areSanitizedTooltipsEqual && areKeysEqual && areCostBurnsEqual && areTooltipsEqual)
            {
                return true;
            }

            return false;
        }
    }
}
