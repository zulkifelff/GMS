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
    public class DepartmentController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        ApplicationConstants applicationConstants = new ApplicationConstants();
        Utilities utilities = new Utilities();
        Response response = new Response();

        [HttpGet]
        public HttpResponseMessage GetDepartments()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.DepartmentRepository.GetDepartments());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetDepartmentById(int? departmentId)
        {
            try
            {
                departmentId = departmentId ?? 0;
                return Request.CreateResponse(HttpStatusCode.OK, uow.DepartmentRepository.GetDepartmentById(departmentId));
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpPost]
        public HttpResponseMessage InsertDepartment(Department department)
        {
            try
            {
                if (department.Title == null || department.Title == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (department.CreatedBy == null || department.CreatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                department.IsActive = true;
                department.CreatedOn = DateTime.Now;
                department.UpdatedOn = department.CreatedOn;
                department.UpdatedBy = department.CreatedBy;
                uow.DepartmentRepository.InsertDepartment(department);
                uow.DepartmentRepository.Save();

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
        public HttpResponseMessage UpdateDepartment(Department department)
        {
            try
            {
                if (department.UpdatedBy == null || department.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (department.Title == null || department.Title == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (department.DepartmentId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                department.UpdatedOn = DateTime.Now;
                uow.DepartmentRepository.UpdateDepartment(department);
                uow.DepartmentRepository.Save();

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
        public HttpResponseMessage DeleteDepartment(Department department)
        {
            try
            {
                if (department.UpdatedBy == null || department.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (department.DepartmentId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                department.UpdatedOn = DateTime.Now;
                uow.DepartmentRepository.DeleteDepartment(department.DepartmentId, (int)department.UpdatedBy);
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