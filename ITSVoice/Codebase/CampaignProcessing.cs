﻿using ITSVoice.Helper;
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

        public static void View_CampaignCDR(int Id, int? callType, string start, string end, int? callResponse, out List<CampaignCDRModel> CampaignCDR, out int totalPages)
        {
            CampaignCDR = new List<CampaignCDRModel>();

            var Records = AppDB.WEB_View_CampaignCDR(Id, callType, start, end, callResponse);

            if (Records.FirstOrDefault() != null)
            {
                CampaignCDR = Records.ToList<CampaignCDRModel>();
            }
            Records.NextResult();
            totalPages = Records.FirstOrDefault().totalPages;
        }
    }
}