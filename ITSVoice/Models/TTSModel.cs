using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ITSVoice.Models
{
    public class TTS : BaseAction
    {
        [Required]
        [JsonProperty(Order = 1)]
        public int InitialDelay { get; set; }
        [Required]
        [JsonProperty(Order = 2)]
        public string Text { get; set; }
        [Required]
        [JsonProperty(Order = 3)]
        public string VoiceName { get; set; }

        public override T GetInstance<T>()
        {
            return (T)Convert.ChangeType(this, typeof(TTS));

        }

        public override void ValidateRequest(Action func)
        {
            func();
        }
    }
}
