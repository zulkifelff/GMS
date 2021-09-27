using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utilities
    {

        public string GetUniqueName()
        {
            DateTime date = DateTime.Now;
            return date.Year-2000 +  "-" + date.Month + "-" + date.Millisecond;
        }
        //public string GetStatusDescription(int statusId)
        //{
        //    if (statusId == 1)
        //        return ApplicationConstants.Pending;
        //    else if (statusId == 2)
        //        return ApplicationConstants.InProgress;
        //    else if (statusId == 3)
        //        return ApplicationConstants.Dispatched;
        //    else if (statusId == 4)
        //        return ApplicationConstants.Completed;
        //    else if (statusId == 5)
        //        return ApplicationConstants.Bounced;
        //    else if (statusId == 6)
        //        return ApplicationConstants.Deleted;
        //    else
        //        return ApplicationConstants.None;

        //}
        //    public string SendRegisterationEmail(string name , string password, string to)
        //    {
        //        string body = string.Format(ApplicationConstants.MRegister, name, password);
        //        return EmailManager.SendHTML(ApplicationConstants.SRegister, body,to);

        //    }
        //    public string SendResetPasswordEmail(string name, string password, string to)
        //    {
        //        string body = string.Format(ApplicationConstants.MResetPassword, name, password);
        //        return EmailManager.SendHTML(ApplicationConstants.SResetPassword, body, to);
        //    }
        //    public string SendSubscriptionEmail(string to)
        //    {
        //        string body = ApplicationConstants.MSubscription;
        //        return EmailManager.SendHTML(ApplicationConstants.SSubscription, body, to);
        //    }
        //    public string SendOrderPlacedEmail(string name, string orderId,string orderCode, string to, string password)
        //    {
        //        string body = string.Format(ApplicationConstants.MOrderPlaced, name,orderId, orderCode,password);
        //        return EmailManager.SendHTML(string.Format(ApplicationConstants.SOrderPlaced,orderId), body, to);
        //    }
        //    public string SendOrderCompletedEmail(string name, string orderId, string orderCode, string to,string password)
        //    {
        //        string body = string.Format(ApplicationConstants.MOrderCompleted, name, orderId,orderCode,password);
        //        return EmailManager.SendHTML(string.Format(ApplicationConstants.SOrderCompleted,orderId), body, to);
        //    }
        //    public string SendSubscriberNewsLetter(string message, string to)
        //    {
        //        return EmailManager.SendHTML(ApplicationConstants.SNewsLetter, message, to);

        //    }
    }
}
