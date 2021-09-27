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
    public class PackageController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        ApplicationConstants applicationConstants = new ApplicationConstants();
        Utilities utilities = new Utilities();
        Response response = new Response();

        [HttpGet]
        public HttpResponseMessage GetPackages()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.PackageRepository.GetPackages());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetPackageById(int? packageId)
        {
            try
            {
                packageId = packageId ?? 0;
                return Request.CreateResponse(HttpStatusCode.OK, uow.PackageRepository.GetPackageById(packageId));
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpPost]
        public HttpResponseMessage InsertPackage(Package package)
        {
            try
            {
                if (package.Amount == null || package.Amount <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidAmount;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.DurationDays == null || package.DurationDays <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidDays;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.Title == null || package.Title == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.CreatedBy == null || package.CreatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                package.IsActive = true;
                package.CreatedOn = DateTime.Now;
                package.UpdatedOn = package.CreatedOn;
                package.UpdatedBy = package.CreatedBy;
                uow.PackageRepository.InsertPackage(package);
                uow.PackageRepository.Save();

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
        public HttpResponseMessage UpdatePackage(Package package)
        {
            try
            {
                if (package.UpdatedBy == null || package.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.Amount == null || package.Amount <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidAmount;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.DurationDays == null || package.DurationDays <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidDays;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.Title == null || package.Title == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.PackageId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                package.UpdatedOn = DateTime.Now;
                uow.PackageRepository.UpdatePackage(package);


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
        public HttpResponseMessage Deletepackage(Package package)
        {
            try
            {
                if (package.UpdatedBy == null || package.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (package.PackageId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                package.UpdatedOn = DateTime.Now;
                uow.PackageRepository.DeletePackage(package.PackageId, (int)package.UpdatedBy);
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