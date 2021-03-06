using ITSVoice.Helper;
using ITSVoice.Models;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSVoice.Codebase
{
    public class CampaignProcessing
    {
        static dynamic AppDB = DataBaseHelper.GetConnection();

        public static void View_Campaigans(int Id, out List<dynamic> campaignModel, out int totalPages)
        {
            campaignModel = new List<dynamic>();

            var Records = AppDB.WEB_View_Campaigns(Id);

            if (Records.FirstOrDefault() != null)
            {
                campaignModel = Records.ToList<dynamic>();
            }
            Records.NextResult();
            totalPages = Records.FirstOrDefault().totalPages;
        }

        public static void View_CampaignCDR(int Id, int? callType, string start, string end, int? callResponse, out List<dynamic> CampaignCDR, out int totalPages)
        {
            CampaignCDR = new List<dynamic>();

            var Records = AppDB.WEB_View_CampaignCDR(Id, callType, start, end, callResponse);

            if (Records.FirstOrDefault() != null)
            {
                CampaignCDR = Records.ToList<dynamic>();
            }
            Records.NextResult();
            totalPages = Records.FirstOrDefault().totalPages;
        }
        public static void CreateCampaign(CampaignModel campaign) 
        {
            AppDB.WEB_CreateCampaign(campaign.UserId, campaign.Name, campaign.Type, campaign.StartDate, campaign.EndDate, campaign.DailyStartTime, campaign.DailyEndTime, campaign.IsIndefinite, campaign.Is24Hours, campaign.Status, campaign.CreatedDateTime, campaign.CallFlow, campaign.Cost, campaign.RemoteIP);
        }
    }
}