using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public class Input : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public int Timeout { get; set; }
        [Required]
        [JsonProperty(Order = 2)]
        public string Type { get; set; }
        public List<SelectListItem> TypeItems { get; set; }
        [Required]
        [JsonProperty(Order = 3)]
        public int Length { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(Input));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
