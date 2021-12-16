using ITSVoice.Helper;
using ITSVoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSVoice.Codebase
{
    public class UserProcessing
    {
        static dynamic AppDB = DataBaseHelper.GetConnection();

        public static void View_Users(int Id, int userId, string username, out List<UserModel> CampaignCDR, out int totalPages)
        {
            CampaignCDR = new List<UserModel>();
            var Records = AppDB.WEB_View_Users(currentPage: Id, Id: userId, username: username);

            if (Records.FirstOrDefault() != null)
            {
                CampaignCDR = Records.ToList<UserModel>();
            }
            Records.NextResult();
            totalPages = Records.FirstOrDefault().totalPages;
        }

        public static void CreateUser(UserModel user) 
        {
            AppDB.WEB_CreateUser(Fullname: user.Fullname, Username: user.Username, Password: user.Password, Email: user.Email, Mobile: user.Mobile, ExpiryDateTime: user.ExpiryDateTime, UserType: user.UserType, AccountType: user.AccountType, Currency: user.Currency, CallRateAmount: user.CallRateAmount, BalanceAmount: user.BalanceAmount, ParentAccountId: user.ParentAccountId, AuthorizedIP: user.AuthorizedIP);
        }

        public static void UpdateUser(UserModel user)
        {
            AppDB.WEB_UpdateUser(user.Id, Fullname: user.Fullname, Username: user.Username, Password: user.Password, Email: user.Email, Mobile: user.Mobile, ExpiryDateTime: user.ExpiryDateTime, UserType: user.UserType, AccountType: user.AccountType, Currency: user.Currency, CallRateAmount: user.CallRateAmount, AuthorizedIP: user.AuthorizedIP, BalanceAmount: user.BalanceAmount);
        }

        public static void BalanceRecharge(UserBalanceRechargeModel userBalanceRechargeModel)
        {
            AppDB.WEB_BalanceRecharge(UserId: userBalanceRechargeModel.UserId, Action: userBalanceRechargeModel.Action, Currency: userBalanceRechargeModel.Currency, Balance: userBalanceRechargeModel.Balance);
        }
    }
}