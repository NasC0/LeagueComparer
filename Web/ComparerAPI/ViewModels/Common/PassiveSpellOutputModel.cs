using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComparerAPI.ViewModels.Common
{
    public class PassiveSpellOutputModel
    {
        public string Description { get; set; }
        public string SanitizedDescription { get; set; }
        public string Name { get; set; }
        public ImageOutputModel Image { get; set; }
    }
}