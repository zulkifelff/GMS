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
    public class EmployeeController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        ApplicationConstants applicationConstants = new ApplicationConstants();
        Utilities utilities = new Utilities();
        Response response = new Response();

        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.EmployeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetEmployeesDetailData()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.EmployeeRepository.GetEmployeesDetailData());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetEmployeeById(int? employeeId)
        {
            try
            {
                employeeId = employeeId ?? 0;
                return Request.CreateResponse(HttpStatusCode.OK, uow.EmployeeRepository.GetEmployeeById(employeeId));
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpPost]
        public HttpResponseMessage InsertEmployee(Employee employee)
        {
            try
            {
                if (employee.Name == null || employee.Name == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.CreatedBy == null || employee.CreatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Address == null || employee.Address == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyAddress;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Gender == null)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyGender;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.CNIC == null || employee.CNIC == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCNIC;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DateOfEmployment == null || employee.DateOfEmployment > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidEmploymentDate;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DOB == null || employee.DOB > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidDOB;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Email == null || employee.Email == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Mobile == null || employee.Mobile == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Phone == null || employee.Phone == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyPhone;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                
                //improve this check logic
                if (employee.Mobile.Length != 11)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                //improve this check logic
                if (employee.Phone.Length != 11)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidPhone;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.ProfilePicture == null || employee.ProfilePicture == "")
                    employee.ProfilePicture = ApplicationConstants.DefaultUserImagePath;


                if (employee.Nationality == null || employee.Nationality <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCountryId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DesignationId == null || employee.DesignationId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.selectDesignation;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DepartmentId == null || employee.DepartmentId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.selectDepartments;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.UserTypeId == null || employee.UserTypeId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyUserTypeId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueEmployeeNumber(employee.EmployeeNumber,0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueEmployeeNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueEmail(employee.Email, 0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueMobile(employee.Mobile, 0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueCnic(employee.CNIC, 0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueCnic;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                employee.Password = ApplicationConstants.DefaultPassword;
                employee.EmployeeCode = Guid.NewGuid().ToString();
                employee.IsActive = true;
                employee.CreatedOn = DateTime.Now;
                employee.UpdatedOn = employee.CreatedOn;
                employee.UpdatedBy = employee.CreatedBy;
                uow.EmployeeRepository.InsertEmployee(employee);
                uow.EmployeeRepository.Save();
                if(employee.EmployeeId >0)
                {
                    employee.EmployeeNumber = (ApplicationConstants.headStart + employee.EmployeeId).ToString();
                    employee.CardNumber = employee.EmployeeNumber;
                    uow.EmployeeRepository.ModifyEmployee(employee);
                    uow.EmployeeRepository.Save();
                }

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
        public HttpResponseMessage UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee.UpdatedBy == null || employee.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.EmployeeId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Name == null || employee.Name == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Address == null || employee.Address == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyAddress;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Gender == null)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyGender;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.CNIC == null || employee.CNIC == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCNIC;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DateOfEmployment == null || employee.DateOfEmployment > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidEmploymentDate;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DOB == null || employee.DOB > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidDOB;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Email == null || employee.Email == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Mobile == null || employee.Mobile == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Phone == null || employee.Phone == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyPhone;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Mobile == null || employee.Mobile == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                
               
                //improve this check logic
                if (employee.Mobile.Length != 11)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                //improve this check logic
                if (employee.Phone.Length != 11)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidPhone;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.ProfilePicture == null || employee.ProfilePicture == "")
                    employee.ProfilePicture = ApplicationConstants.DefaultUserImagePath;

                if (employee.Nationality == null || employee.Nationality <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCountryId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DesignationId == null || employee.DesignationId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.selectDesignation;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.DepartmentId == null || employee.DepartmentId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.selectDepartments;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.CardNumber == null || employee.CardNumber == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCardNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.UserTypeId == null || employee.UserTypeId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyUserTypeId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueEmployeeNumber(employee.EmployeeNumber, employee.EmployeeId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueEmployeeNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueEmail(employee.Email, employee.EmployeeId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueMobile(employee.Mobile, employee.EmployeeId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.EmployeeRepository.IsUniqueCnic(employee.CNIC, employee.EmployeeId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueCnic;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                employee.UpdatedOn = DateTime.Now;
                uow.EmployeeRepository.UpdateEmployee(employee);
                uow.EmployeeRepository.Save();

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
        public HttpResponseMessage UpdateCredentials(Employee employee)
        {
            try
            {
                if (employee.UpdatedBy == null || employee.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.EmployeeNumber == null || employee.EmployeeNumber == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyEmployeeNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.Password == null || employee.Password == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyPassword;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.EmployeeId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                employee.UpdatedOn = DateTime.Now;
                uow.EmployeeRepository.UpdateCredentials(employee);
                uow.EmployeeRepository.Save();

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
        public HttpResponseMessage DeleteEmployee(Employee employee)
        {
            try
            {
                if (employee.UpdatedBy == null || employee.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (employee.EmployeeId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                employee.UpdatedOn = DateTime.Now;
                uow.EmployeeRepository.DeleteEmployee(employee.EmployeeId, (int)employee.UpdatedBy);
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