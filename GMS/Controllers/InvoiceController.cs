using Common;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GMS.Controllers
{
    [BasicAuthentication]
    public class GeneralController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        ApplicationConstants applicationConstants = new ApplicationConstants();
        Utilities utilities = new Utilities();
        Response response = new Response();
        // GET api/<controller>
        [HttpPost]
        public HttpResponseMessage GenerateInvoice(Invoice invoice)
        {
            try
            {
                if (invoice.CreatedBy == null || invoice.CreatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (invoice.MemberId == null || invoice.MemberId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                if (invoice.AdminFee == null)
                    invoice.AdminFee = 0;

                if (invoice.IsRenewal == null)
                    invoice.IsRenewal = false;

                if (invoice.Amount == null || invoice.Amount == 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidAmount;
                    return Request.CreateResponse(HttpStatusCode.OK, response);

                }

                if (invoice.StartDate == null || invoice.StartDate > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidStartDate;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                if (invoice.PackageId == null || invoice.PackageId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyPackageId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (invoice.PaymentMethodId == null || invoice.PaymentMethodId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyPaymentId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                //calculate end date
                //calculate InvoiceNumber


                invoice.UpdatedBy = invoice.CreatedBy;

                invoice.CreatedOn = DateTime.Now;
                invoice.UpdatedOn = invoice.CreatedOn;
                invoice.IsActive = true;

                uow.GeneralRepository.InsertInvoice(invoice);
                uow.GeneralRepository.Save();

                response.Code = ApplicationConstants.successCode;
                response.Message = ApplicationConstants.generalSuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
    }
}