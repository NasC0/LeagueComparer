using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ComparerAPI.ViewModels.Common;
using Models;
using Models.Common;
using Models.JsonConverters;
using Newtonsoft.Json;

namespace ComparerAPI.ViewModels
{
    public class MasteryOutputModel
    {
        public static Expression<Func<Mastery, MasteryOutputModel>> FromModel
        {
            get
            {
                return model => new MasteryOutputModel()
                {
                    Ranks = model.Ranks,
                    MasteryId = model.MasteryId,
                    Description = model.Description,
                    SanitizedDescription = model.SanitizedDescription,
                    Name = model.Name,
                    Image = new ImageOutputModel()
                    {
                        Name = model.Image.Name,
                        Height = model.Image.Height,
                        Width = model.Image.Width,
                        Sprite = model.Image.Sprite
                    },
                    Prerequisites = model.Prerequisite,
                    MasteryTree = model.MasteryTree
                };
            }
        }

        public int Ranks { get; set; }
        public int MasteryId { get; set; }
        public IEnumerable<string> Description { get; set; }
        public IEnumerable<string> SanitizedDescription { get; set; }
        public string Name { get; set; }
        public ImageOutputModel Image { get; set; }
        public string Prerequisites { get; set; }
        [JsonConverter(typeof(GenericEnumConverter<MasteryTypes>))]
        public MasteryTypes MasteryTree { get; set; }
    }
}