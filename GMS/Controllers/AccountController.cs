using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using DAL.ViewModels;
using DAL;
using DAL.Models;
using Common;
using System.Web.Http.Cors;

namespace GMS.Controllers
{
    //[EnableCors(origins: "http://localhost:4200/", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        Response response = new Response();
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login(LoginModel login)
        {
            if (login.EmployeeNumber == null || login.EmployeeNumber =="")
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ApplicationConstants.EmptyEmployeeNumber;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            if (login.Password == null || login.Password == "")
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ApplicationConstants.EmptyPassword;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            if(uow.EmployeeRepository.Validate(login.EmployeeNumber,login.Password)!=null)
            {
                string strOriginal = login.EmployeeNumber + ":" + login.Password;
                byte[] byt = System.Text.Encoding.UTF8.GetBytes(strOriginal);

                // convert the byte array to a Base64 string
                login.Token = "Basic " + Convert.ToBase64String(byt);
                login.Id = 1;
                return Request.CreateResponse(HttpStatusCode.OK, login);

            }
            else
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ApplicationConstants.invalidCredentials;
                return Request.CreateResponse(HttpStatusCode.OK, response);

            }
        }
    }
}