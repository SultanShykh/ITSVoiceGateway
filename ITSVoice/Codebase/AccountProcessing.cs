using ITSVoice.Helper;
using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSVoice.Codebase
{
    public class AccountProcessing
    {
        static dynamic AppDB = DataBaseHelper.GetConnection();

        public dynamic WEB_UserAuth(string username, string password) 
        {
            var rec = AppDB.WEB_UserAuth(Username: username, Password: password).FirstOrDefault();
            return rec;
        }
    }
}