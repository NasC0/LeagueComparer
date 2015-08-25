using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Models.Common.Champion
{
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
                    var effectListToken = effect as JToken;
                    var effectList = JsonConvert.DeserializeObject<IEnumerable<double>>(effectListToken.ToString());
                    currentSpellEffect.Value = effectList;
                }

                spellEffects.Add(currentSpellEffect);
                spellEffectsCounter++;
            }

            this.Effect = spellEffects;
        }
    }
}
