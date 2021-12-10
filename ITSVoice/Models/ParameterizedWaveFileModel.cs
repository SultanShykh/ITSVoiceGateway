
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public class ParameterizedWaveFile : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public int InitialDelay { get; set; }
        [JsonProperty(Order = 2)]
        public string OptionalFolder { get; set; }
        [JsonIgnore]
        public List<SelectListItem> OptionalFolderItems { get; set; }
        [Required]
        [JsonProperty(Order = 3)]
        public string ParameterIdentifier { get; set; }
        [Required]
        [JsonProperty(Order = 4)]
        public string ParameterType { get; set; }
        [JsonIgnore]
        public List<SelectListItem> ParameterTypeItems { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(ParameterizedWaveFile));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
