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
        public string CallType { get; set; }
        public List<BaseAction> CallFlowQueue { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DailyStartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DailyEndTime { get; set; }

        [Required]
        public int IsIndefinite { get; set; }

        [Required]
        public int Is24Hours { get; set; }

        [Required]
        public int Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CallFlow { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public string RemoteIP { get; set; }

        [Required]
        public List<IDictionary<string, string>> CallActions { get; set; }
    }
}