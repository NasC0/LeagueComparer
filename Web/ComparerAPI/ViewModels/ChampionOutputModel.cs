using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComparerAPI.ViewModels.Common;
using Helpers;
using Models;
using Models.Common.Champion;
using Models.Common.Enumerations;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace ComparerAPI.ViewModels
{
    public class ChampionOutputModel
    {
        public static Expression<Func<Champion, ChampionOutputModel>> FromModel
        {
            get
            {
                return model => new ChampionOutputModel()
                {
                    ChampionId = model.ChampionId,
                    Name = model.Name,
                    Title = model.Title,
                    Partype = model.Partype,
                    Stats = model.Stats,
                    Passive = new PassiveSpellOutputModel()
                    {
                        Name = model.Passive.Name,
                        Description = model.Passive.Description,
                        SanitizedDescription = model.Passive.SanitizedDescription,
                        Image = new ImageOutputModel()
                        {
                            Name = model.Passive.Image.Name,
                            Height = model.Passive.Image.Height,
                            Width = model.Passive.Image.Width,
                            Sprite = model.Passive.Image.Sprite,
                            ImageUrl = ApiUrlBuilder.GetPassiveSpellImageUrl(model.Passive.Image.Name)
                        }
                    },
                    Image = new ImageOutputModel()
                    {
                        Name = model.Image.Name,
                        Height = model.Image.Height,
                        Width = model.Image.Width,
                        Sprite = model.Image.Sprite,
                        ImageUrl = ApiUrlBuilder.GetChampionImageUrl(model.Image.Name)
                    },
                    Spells = model.Spells.AsQueryable().Select(SpellOutputModel.FromModel)
                };
            }
        }

        public int ChampionId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        [JsonConverter(typeof(GenericEnumConverter<ChampionResourceType>))]
        public ChampionResourceType Partype { get; set; }
        public ChampionStats Stats { get; set; }
        public PassiveSpellOutputModel Passive { get; set; }
        public ImageOutputModel Image { get; set; }
        public IEnumerable<SpellOutputModel> Spells { get; set; }

    }
}