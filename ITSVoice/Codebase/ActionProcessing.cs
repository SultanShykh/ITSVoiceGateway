using ITSVoice.Helper;
using ITSVoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSVoice.Codebase
{
    public class ActionProcessing
    {
        static dynamic AppDB = DataBaseHelper.GetConnection();
        public static string GetActionData() 
        {
            var result = AppDB.WEB_GetActionData().FirstOrDefault();
            return result.Json;
        }
    }
}