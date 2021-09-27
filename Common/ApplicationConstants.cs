using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ApplicationConstants
    {
        public const string EmployeeImagePath = "/Files/Employees/";
        public const string MemberImagePath = "/Files/Members/";
        public const int headStart = 999;
        public const string empPrefix = "E-";
        public const string memPrefix = "M-";
        public const string invPrefix = "M-";
        public const string DefaultPassword = "abc123";
        public string [] imageSupportedTypes = new[] { "image/png", "image/jpeg", "image/bmp" };
        public const int maxUserImageSize = 50;
        public const int maxProductImageSize =50;

        public const int AdminTypeId = 1;
        public const int UserTypeId = 2;

        public const int errorCode = 0;
        public const int successCode = 1;
        public const int exceptionCode = -1;

        
        public const string DefaultUserImagePath = "";

        public const int MemberPageSize = 20;
        
        
        #region General Message
        public const string generalSuccessMsg = "Operations performed successfully!";
        public const string generalFailureMsg = "Unable to perform action. Contact your support administrator!";
        #endregion

        #region Error Messages
        public const string selectDesignation = "Please Select Designation.";
        public const string selectDepartments = "Please Select Departments.";
        public const string selectCorrectImageTypes = "Please upload png,jpeg or bmp image.";
        public const string maxUserImageSizeMsg = "Image size cannot be larger than 50MB.";
        public const string uniqueEmail = "Email already exists.";
        public const string uniqueMobile = "Mobile already exists.";
        public const string invalidUserId = "Invalid User Id.";
        public const string invalidPackageId = "Invalid Package Id.";
        public const string uniqueCnic = "CNIC already exists!";
        public const string uniqueEmployeeNumber = "Employee Number already exists.";
        public const string uniqueMembershipNumber = "Membership Number already exists.";
        public const string invalidAmount = "Invalid Amount. Please enter correct Amount.";
        public const string invalidDays = "Must be a a valid no of days > 0";
        public const string invalidEmploymentDate = "Employment date must be a past date.";
        public const string invalidStartDate = "Start date must be a past date.";
        public const string invalidCredentials = "Invalid credentials, please check your employee number or password";
        public const string invalidDOB = "DOB must be a past date.";
        public const string emptyUserTypeId = "Empty UserType Id.";
        public const string emptyGender = "Empty Gender.";
        public const string invalidPhone = "Phone number must be 11 characters.";
        public const string invalidMobile = "Mobile number must be 11 characters.";
        public const string emptyCountryId = "Country Id is Empty.";
        public const string emptyPhone = "Phone is Empty.";
        public const string emptyMobile = "Mobile is Empty.";
        public const string emptyEmail = "Email is Empty.";
        public const string emptyCNIC = "CNIC is Empty.";
        public const string emptyCardNumber = "Card Number is Empty!";
        public const string EmptyAddress = "Address is empty.";
        public const string EmptyPassword = "Please Enter Password";
        public const string EmptyEmployeeNumber = "Please enter Employee  Number.";
        public const string EmptyId = "Please Enter ID";
        public const string EmptyPackageId = "Please Enter Package Id";
        public const string EmptyPaymentId = "Please Enter Payment Method";
        public const string EmptyTitle = "Please Enter Title";

        #endregion

        //#region EMAIL SMTP
        //public const string HOST = "smtp.ionos.com";
        //public const int PORT = 587; //465 for ssl , 587 for unsecured
        //public const string FROMNAME = "Food4All - Support";
        //public const string SMTP_USERNAME = "support@leatherproductions.com";
        //public const string SMTP_PASSWORD= "support@Admin123";
        //#endregion

        //#region EMAIL Messages
        //public const string SRegister = "Food4All - Successful Registeration";
        //public const string MRegister = "Dear <span style=\"font-weight:bold\">{0}</span>,<br><br> You have successfully register to Food4All. Your password is <span style=\"font-weight:bold\">{1}</span>. Feel free to login anytime for more better service! <br> Have a great day! <br> <br> Regards, <br> Team Food4All";

        //public const string SResetPassword = "Food4All - Reset Password";
        //public const string MResetPassword = "Dear <span style=\"font-weight:bold\">{0}</span>,<br><br> Your new password is <span style=\"font-weight:bold\">{1}</span>. Feel free to login anytime for more better service! <br> Have a great day!  <br> <br> Regards, <br> Team Food4All";

        //public const string SOrderPlaced = "Food4All - Order # {0}";
        //public const string MOrderPlaced = "Dear <span style=\"font-weight:bold\">{0}</span>,<br><br> Your order has been placed successfully by Order # {1}.<br>Click on following link to track your order! <br> Order Tracking : <span style=\"font-weight:bold\">http://food4all.bunchhive.com/myOrder/{2}</span> . <br> Feel free to login anytime for more better service!<br> Visit http://food4all.bunchhive.com/Home/CustomerProfile and enter your email.  Your Current Password is <span style=\"font-weight:bold\">{3}</span> <br> Have a great day! <br> <br> Regards, <br> Team Food4All";

        //public const string SOrderCompleted = "Food4All - Order # {0}";
        //public const string MOrderCompleted = "Dear <span style=\"font-weight:bold\">{0}</span>,<br><br> We are pleased to tell you that your  Order # {1} has been delivered successfully to you.<br>Click on following link to track your order! <br> Order Tracking : <span style=\"font-weight:bold\">http://food4all.bunchhive.com/myOrder/{2}</span> .<br>  Feel free to login anytime for more better service!<br> Visit http://food4all.bunchhive.com/Home/CustomerProfile and enter your email.<br>  Your Current Password is <span style=\"font-weight:bold\">{3}</span>  <br> Have a great day! <br> <br> Regards, <br> Team Food4All";

        //public const string SSubscription = "Food4All - Subscription";
        //public const string MSubscription = "Dear <span style=\"font-weight:bold\">Sir/Madam</span>,<br><br> Thank you so much for subscribing. You will be getting newsletters in the email. Feel free to login/register anytime for more better service! <br> Have a great day!  <br> <br> Regards, <br> Team Food4All";

        //public const string SNewsLetter = "Food4All - NewsLetter";

        //#endregion
    }
}
