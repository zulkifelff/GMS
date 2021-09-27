using Common;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GMS.Controllers
{
    [BasicAuthentication]
    [EnableCors(origins: "http://localhost:4200/", headers: "*", methods: "*")]
    public class DesignationController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        ApplicationConstants applicationConstants = new ApplicationConstants();
        Utilities utilities = new Utilities();
        Response response = new Response();

        [HttpGet]
        public HttpResponseMessage GetDesignations()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.DesignationRepository.GetDesignations());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetDesignationById(int? designationId)
        {
            try
            {
                designationId = designationId ?? 0;
                return Request.CreateResponse(HttpStatusCode.OK, uow.DesignationRepository.GetDesignationById(designationId));
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpPost]
        public HttpResponseMessage InsertDesignation(Designation designation)
        {
            try
            {
                if (designation.Title == null || designation.Title == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (designation.CreatedBy == null || designation.CreatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                designation.IsActive = true;
                designation.CreatedOn = DateTime.Now;
                designation.UpdatedOn = designation.CreatedOn;
                designation.UpdatedBy = designation.CreatedBy;
                uow.DesignationRepository.InsertDesignation(designation);
                uow.DesignationRepository.Save();

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
        [HttpPost]
        public HttpResponseMessage UpdateDesignation(Designation designation)
        {
            try
            {
                if (designation.UpdatedBy == null || designation.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (designation.Title == null || designation.Title == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (designation.DesignationId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                designation.UpdatedOn = DateTime.Now;
                uow.DesignationRepository.UpdateDesignation(designation);

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
        [HttpPost]
        public HttpResponseMessage DeleteDesignation(Designation designation)
        {
            try
            {
                if (designation.UpdatedBy == null || designation.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (designation.DesignationId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                designation.UpdatedOn = DateTime.Now;
                uow.DesignationRepository.DeleteDesignation(designation.DesignationId,(int)designation.UpdatedBy);
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