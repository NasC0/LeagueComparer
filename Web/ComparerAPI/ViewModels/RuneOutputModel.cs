using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ComparerAPI.ViewModels.Common;
using Microsoft.Ajax.Utilities;
using Models;
using Models.Common;

namespace ComparerAPI.ViewModels
{
    public class RuneOutputModel
    {
        public static Expression<Func<Rune, RuneOutputModel>> FromModel
        {
            get
            {
                return model => new RuneOutputModel()
                {
                    Name = model.Name,
                    Description = model.Description,
                    SanitizedDescription = model.Description,
                    Tags = model.Tags,
                    RuneType = model.RuneType,
                    RuneId = model.RuneId,
                    Stats = model.Stats,
                    Image = new ImageOutputModel()
                    {
                        Name = model.Image.Name,
                        Height = model.Image.Height,
                        Width = model.Image.Width,
                        Sprite = model.Image.Sprite
                    }
                };
            }
        }

        public IEnumerable<string> Tags { get; set; }
        public int RuneId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SanitizedDescription { get; set; }
        public IEnumerable<Stat> Stats { get; set; }
        public ImageOutputModel Image { get; set; }
        public RuneType RuneType { get; set; }
    }
}