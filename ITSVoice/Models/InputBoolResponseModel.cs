using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public class InputBoolResponse : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public int InitialDelay { get; set; }
        [Required]
        [JsonProperty(Order = 2)]
        public string TrueFilePath { get; set; }
        [JsonIgnore]
        public List<SelectListItem> TrueFilePathItems { get; set; }
        [Required]
        [JsonProperty(Order = 3)]
        public string FalseFilePath { get; set; }
        [JsonIgnore]
        public List<SelectListItem> FalseFilePathItems { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(InputBoolResponse));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
