using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITSVoice.Models
{
    public class CampaignModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DailyStartTime { get; set; }
        public DateTime? DailyEndTime { get; set; }
        [Required]
        public int IsIndefinite { get; set; }
        [Required]
        public int Is24Hours { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CallFlow { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public string RemoteIP { get; set; }
        public List<IDictionary<string, string>> CallActions { get; set; }
    }
}