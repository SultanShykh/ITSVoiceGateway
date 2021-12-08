using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public class Beep : BaseAction
    {
        [JsonProperty(Order = 1)]
        public string OptionalFilePath { get; set; }
        [JsonIgnore]
        [JsonProperty(Order = 2)]
        public List<SelectListItem> OptionalFilePathItems { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(Beep));
        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
