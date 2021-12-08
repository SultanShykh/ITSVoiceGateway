
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public class InputStringResponseWaveFile : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public int InitialDelay { get; set; }
        [Required]
        [JsonProperty(Order = 2)]
        public string FilePath { get; set; }
        [JsonIgnore]
        public List<SelectListItem> FilePathItems { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(InputStringResponseWaveFile));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
