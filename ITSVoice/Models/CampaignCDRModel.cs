using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSVoice.Models
{
    public class CampaignCDRModel
    {
        public int Id { get; set; } 
        public int CampaignId { get; set; }
        public int CallType { get; set; }
        public string PartyA { get; set; }
        public string PartyB { get; set; }
        public string PartyC { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public DateTime CallStartDateTime  { get; set; }
        public DateTime CallPickDateTime { get; set; }
        public DateTime CallDuration { get; set; }
        public int CallResponse { get; set; }
        public int CallRetries { get; set; }
        public int CallCost { get; set; }
        public Decimal CallRateAmount { get; set; }
        public int CallbackId { get; set; }
        public string CallInput { get; set; }
        public string APIParams { get; set; }
        public string RemoteIP { get; set; }
    }
}