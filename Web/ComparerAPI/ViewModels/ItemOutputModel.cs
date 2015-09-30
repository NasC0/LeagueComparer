using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApiProcessing.Models.JsonConverters;
using ComparerAPI.ViewModels.Common;
using Helpers;
using Models;
using Models.Common;
using Newtonsoft.Json;

namespace ComparerAPI.ViewModels
{
    public class ItemOutputModel
    {
        public static Expression<Func<Item, ItemOutputModel>> FromModel
        {
            get
            {
                return model => new ItemOutputModel()
                {
                    ItemId = model.ItemId,
                    Name = model.Name,
                    Tags = model.Tags,
                    SanitizedDescription = model.SanitizedDescription,
                    Description = model.Description,
                    Image = new ImageOutputModel()
                    {
                        Name = model.Image.Name,
                        Width = model.Image.Width,
                        Height = model.Image.Height,
                        Sprite = model.Image.Sprite,
                        ImageUrl = ApiUrlBuilder.GetItemImageUrl(model.Image.Name)
                    },
                    Price = model.Gold,
                    From = model.From,
                    Into = model.Into,
                    Stats = model.Stats,
                    Group = model.Group,
                    Depth = model.Depth
                };
            }
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string SanitizedDescription { get; set; }
        public string Description { get; set; }
        public ImageOutputModel Image { get; set; }
        public Gold Price { get; set; }
        public IEnumerable<int> From { get; set; }
        public IEnumerable<int> Into { get; set; }
        [JsonConverter(typeof(StatsJsonConverter))]
        public IEnumerable<Stat> Stats { get; set; }
        public string Group { get; set; }
        public int Depth { get; set; }
    }
}