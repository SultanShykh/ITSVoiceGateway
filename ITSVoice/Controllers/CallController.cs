using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSVoice.Controllers
{
    public class CallController : Controller
    {
        public ActionResult SingleCall()
        {
            return View();
        }
        public ActionResult BulkCall()
        {
            return View();
        }
    }
}