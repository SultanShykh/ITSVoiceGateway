
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITSVoice.Models
{
    public class InputVerify : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public string VerificationType { get; set; }
        [Required]
        [JsonProperty(Order = 2)]
        public int VerificationTimeout { get; set; }
        public List<SelectListItem> VerificationTypeItems { get; set; }
        [Required]
        [JsonProperty(Order = 3)]
        public string ResponseType { get; set; }
        public List<SelectListItem> ResponseTypeItems { get; set; }
        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(InputVerify));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
