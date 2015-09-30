using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Helpers;
using Models.Common.Champion;

namespace ComparerAPI.ViewModels.Common
{
    public class SpellOutputModel
    {
        public static Expression<Func<Spell, SpellOutputModel>> FromModel
        {
            get
            {
                return model => new SpellOutputModel()
                {
                    Range = model.Range,
                    LevelTip = model.LevelTip,
                    Resource = model.Resource,
                    MaxRank = model.MaxRank,
                    EffectBurn = model.EffectBurn,
                    Image = new ImageOutputModel()
                    {
                        Name = model.Image.Name,
                        Height = model.Image.Height,
                        Width = model.Image.Width,
                        Sprite = model.Image.Sprite,
                        ImageUrl = ApiUrlBuilder.GetChampionSpellImageUrl(model.Image.Name)
                    },
                    Cooldown = model.Cooldown,
                    Cost = model.Cost,
                    Vars = model.Vars,
                    SanitizedDescription = model.SanitizedDescription,
                    RangeBurn = model.RangeBurn,
                    CostType = model.CostType,
                    Effect = GetSpellEffects(model.EffectOriginal),
                    CooldownBurn = model.CooldownBurn,
                    Description = model.Description,
                    Name = model.Name,
                    SanitizedTooltip = model.SanitizedTooltip,
                    Key = model.Key,
                    CostBurn = model.CostBurn,
                    Tooltip = model.Tooltip
                };
            }
        }

        public static Dictionary<string, IEnumerable<double>> GetSpellEffects(IEnumerable<object> effects)
        {
            var dictionary = new Dictionary<string, IEnumerable<double>>();
 
            if (effects != null)
            {
                var resultEffects = new List<SpellEffectOutputModel>();
                int hitCount = 0;

                foreach (var effect in effects)
                {
                    string key = 'e' + hitCount.ToString();
                    var effectAsList = (IEnumerable<object>)effect;
                    var values = effectAsList != null ? effectAsList.Select(e => (double)e).ToList() : new List<double>();
                    dictionary[key] = values;

                    hitCount++;
                }
            }

            return dictionary;
        }

        public dynamic Range { get; set; }
        public LevelTip LevelTip { get; set; }
        public string Resource { get; set; }
        public int MaxRank { get; set; }
        public IEnumerable<string> EffectBurn { get; set; }
        public ImageOutputModel Image { get; set; }
        public IEnumerable<double> Cooldown { get; set; }
        public IEnumerable<int> Cost { get; set; }
        public IEnumerable<SpellVars> Vars { get; set; }
        public string SanitizedDescription { get; set; }
        public string RangeBurn { get; set; }
        public string CostType { get; set; }
        public Dictionary<string, IEnumerable<double>> Effect { get; set; }
        public string CooldownBurn { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string SanitizedTooltip { get; set; }
        public string Key { get; set; }
        public string CostBurn { get; set; }
        public string Tooltip { get; set; }
    }
}