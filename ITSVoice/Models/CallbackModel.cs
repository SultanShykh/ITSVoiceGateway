
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ITSVoice.Models
{
    public class Callback : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public int DisconnectDelay { get; set; }
        [Required]
        [JsonProperty(Order = 2)]
        public int CallbackDelay { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(Callback));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
