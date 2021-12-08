using ITSVoice.Codebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSVoice.Controllers
{
    public class AccountController : Controller
    {
        AccountProcessing ac = new AccountProcessing();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var rec = ac.WEB_UserAuth(collection["username"], collection["password"]);

            if (rec.statusCode == "false")
                ViewBag.message = rec.Response;
            else 
            {
                Session["userId"] = rec.Id;
                Session["username"] = rec.username;
                Session["userType"] = rec.UserType;

                return RedirectToAction("CampaignCDR", "Campaign");
            }

            return View();
        }
        public ActionResult Logout() 
        {
            Session["userId"] = Session["username"] = Session["userType"] = null;
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index","Account");
        }
    }
}