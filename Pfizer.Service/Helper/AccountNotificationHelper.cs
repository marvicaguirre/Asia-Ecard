using System;
using System.Collections.Generic;
using System.Text;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;

namespace Pfizer.Service.Helper
{
    public class AccountNotificationHelper : NotificationHelper
    {
        public AccountNotificationHelper(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public void NotifyForgotPassword(int userId, bool forEmailNotif, bool forSmsNotif, bool forDashboardNotif, params object[] args)
        {
            Notify("ForgotPassword",
                   new ActivityNotification
                   {
                       Area = "Common",
                       Controller = "Account",
                       Action = "Index",
                       UserId = userId
                   },
                   false,
                   forEmailNotif,
                   forSmsNotif,
                   forDashboardNotif,
                   args: args);
        }


        public void NotifyNewAccount(string userName, string passWord, string emailAddress, string urlLogin)
        {
            const string subject = "New Created Account";

            var messagesBuilder = new StringBuilder();
            messagesBuilder.AppendLine("Hi New Member");
            messagesBuilder.AppendLine("Your new created Login account details.");
            messagesBuilder.AppendLine(string.Format("Your Username:{0} and Password:{1} ", userName, passWord));
            messagesBuilder.AppendLine("Registration is now Complete you may know check you Username and Password");
            messagesBuilder.AppendLine(string.Format("Please login your account by clicking <a href={0}>here", urlLogin));

            var listEmail = new List<string>();
            listEmail.Add(emailAddress);

            NotifyByEmail(subject, messagesBuilder.ToString(), listEmail);

        }

        

    }
}
