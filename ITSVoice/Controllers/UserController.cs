using ITSVoice.Codebase;
using ITSVoice.Helper;
using ITSVoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITSVoice.Controllers
{
    //[Auth("admin", "master")]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int Id=1, string username=null)
        {
            username = string.IsNullOrEmpty(username) ? null:username;
            UserProcessing.View_Users(Id, Convert.ToInt32(Session["userId"]), username, out List<UserModel> users, out int totalPages);

            ViewBag.PageNumber = Id;
            ViewBag.username = username;
            ViewBag.totalPages = totalPages;
            return View(users);
        }

        [ChildActionOnly]
        public ActionResult _AddUpdate() 
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _AddUpdate(UserModel user)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
                return Json(new { status = true, message = "Please Fill the form" });
            try
            {
                if (user.UserType.Equals("admin")) user.ParentAccountId = null;
                else user.ParentAccountId = Convert.ToInt32(Session["UserId"]);

                UserProcessing.CreateUser(user);
                return Json(new { status = true, message = "Successfully Created" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message.ToString() });
            }
        }

        [ChildActionOnly]
        public ActionResult _BalanceRecharge()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _BalanceRecharge(UserBalanceRechargeModel user)
        {
            if (!ModelState.IsValid)
                return Json(new { status = true, message = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) });

            try
            {
                UserProcessing.BalanceRecharge(user);
                return Json(new { status = true, message = "Successfully Updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message.ToString() });
            }
        }

        [HttpPost]
        public ActionResult _UpdateUser(UserModel user)
        {
            ModelState.Remove("Password");
            if (!ModelState.IsValid)
                return Json(new { status = true, message = "Please Fill the form" });
            try
            {
                user.ParentAccountId = Convert.ToInt32(Session["UserId"]);
                UserProcessing.UpdateUser(user);
                return Json(new { status = true, message = "Successfully Updated" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message.ToString() });
            }
        }
    }
}