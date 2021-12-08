using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSVoice.Helper
{
    public class DataBaseHelper
    {   
        public static dynamic GetConnection() 
        {
            var db = Database.OpenNamedConnection("AppDB");
            return db;
        }
    }
}