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
    public class MemberController : ApiController
    {
        UnitOfWork uow = new UnitOfWork(new GMSEntities());
        ApplicationConstants applicationConstants = new ApplicationConstants();
        Utilities utilities = new Utilities();
        Response response = new Response();

        [HttpGet]
        public HttpResponseMessage GetMembers()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.MemberRepository.GetMembers());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetMembersDetailData()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.MemberRepository.GetMembersDetailData());
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpGet]
        public HttpResponseMessage GetMemberById(int? memberId)
        {
            try
            {
                memberId = memberId ?? 0;
                return Request.CreateResponse(HttpStatusCode.OK, uow.MemberRepository.GetMemberById(memberId));
            }
            catch (Exception ex)
            {
                response.Code = ApplicationConstants.errorCode;
                response.Message = ex.InnerException + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }
        [HttpPost]
        public HttpResponseMessage InsertMember(Member member)
        {
            try
            {
                if (member.Name == null || member.Name == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.CreatedBy == null || member.CreatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Address == null || member.Address == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyAddress;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Gender == null)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyGender;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.CNIC == null || member.CNIC == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCNIC;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                //if (member.MembershipNumber == null || member.MembershipNumber=="")
                //{
                //    response.Code = ApplicationConstants.errorCode;
                //    response.Message = ApplicationConstants.uniqueMembershipNumber;
                //    return Request.CreateResponse(HttpStatusCode.OK, response);
                //}
                if (member.DOB == null || member.DOB > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidDOB;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Email == null || member.Email == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Mobile == null || member.Mobile == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                
                
                //improve this check logic
                if (member.Mobile.Length != 11)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                //improve this check logic
                
                if (member.ProfilePicture == null || member.ProfilePicture == "")
                    member.ProfilePicture = ApplicationConstants.DefaultUserImagePath;


                if (member.Nationality == null || member.Nationality <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCountryId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                
                if (!(uow.MemberRepository.IsUniqueMembershipNumber(member.MembershipNumber,0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMembershipNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.MemberRepository.IsUniqueEmail(member.Email, 0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.MemberRepository.IsUniqueMobile(member.Mobile, 0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.MemberRepository.IsUniqueCnic(member.CNIC, 0)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueCnic;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                member.IsActive = true;
                member.CreatedOn = DateTime.Now;
                member.UpdatedOn = member.CreatedOn;
                member.UpdatedBy = member.CreatedBy;

                //check missing for 1 invoices
                //Currently break statement has been added here
                foreach (var data in member.Invoices)
                {
                    //amount from package
                    //if (data.Amount == null || data.Amount == 0)
                    //{
                    //    response.Code = ApplicationConstants.errorCode;
                    //    response.Message = ApplicationConstants.invalidAmount;
                    //    return Request.CreateResponse(HttpStatusCode.OK, response);

                    //}
                    if (data.StartDate == null || data.StartDate > DateTime.Now)
                    {
                        response.Code = ApplicationConstants.errorCode;
                        response.Message = ApplicationConstants.invalidStartDate;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }

                    if (data.PackageId == null || data.PackageId <= 0)
                    {
                        response.Code = ApplicationConstants.errorCode;
                        response.Message = ApplicationConstants.EmptyPackageId;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    if (data.PaymentMethodId == null || data.PaymentMethodId <= 0)
                    {
                        response.Code = ApplicationConstants.errorCode;
                        response.Message = ApplicationConstants.EmptyPaymentId;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }


                    if (data.AdminFee == null)
                        data.AdminFee = 0;
                    if (data.RegistrationFee == null)
                        data.RegistrationFee = 0;

                    if (data.Comments == null)
                        data.Comments = "";
                    if (data.Discount == null)
                        data.Discount = 0;

                    //always false in this case
                    //if (data.IsRenewal == null)
                    //    data.IsRenewal = false;

                    data.IsRenewal = false;
                    data.CreatedBy = member.CreatedBy;
                    data.CreatedOn = member.CreatedOn;
                    data.UpdatedBy = member.CreatedBy;
                    data.UpdatedOn = member.CreatedOn;
                    Package package = uow.PackageRepository.GetPackageById(data.PackageId);
                    if(package!=null)
                    {
                        data.EndDate = data.StartDate.Value.AddDays(package.DurationDays == null ? 0 : (double)package.DurationDays);
                        data.Amount = package.Amount == null ? 0 : package.Amount;
                    }
                    else
                    {
                        response.Code = ApplicationConstants.errorCode;
                        response.Message = ApplicationConstants.invalidPackageId;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    data.Total = data.Amount + data.RegistrationFee + data.AdminFee - data.Discount;

                    data.IsActive = true;
                    data.InvoiceNumber =  ApplicationConstants.invPrefix +  utilities.GetUniqueName();
                    //or add break here so that it will run one time
                    break;
                }
                uow.MemberRepository.InsertMember(member);
                uow.MemberRepository.Save();
                if(member.MemberId >0)
                {
                    //generate  member number and assign it to card number as well.
                    //starting value to be 999


                    member.MembershipNumber = ApplicationConstants.memPrefix + (ApplicationConstants.headStart + member.MemberId).ToString();
                    uow.MemberRepository.ModifyMember(member);
                    uow.MemberRepository.Save();
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
        public HttpResponseMessage UpdateMember(Member member)
        {
            try
            {
                if (member.UpdatedBy == null || member.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.MemberId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Name == null || member.Name == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyTitle;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Address == null || member.Address == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyAddress;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Gender == null)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyGender;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.CNIC == null || member.CNIC == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCNIC;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.MembershipNumber == null || member.MembershipNumber == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMembershipNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.DOB == null || member.DOB > DateTime.Now)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidDOB;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Email == null || member.Email == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.Mobile == null || member.Mobile == "")
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }


                //improve this check logic
                if (member.Mobile.Length != 11)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                

                if (member.ProfilePicture == null || member.ProfilePicture == "")
                    member.ProfilePicture = ApplicationConstants.DefaultUserImagePath;


                if (member.Nationality == null || member.Nationality <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.emptyCountryId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }

                if (!(uow.MemberRepository.IsUniqueMembershipNumber(member.MembershipNumber, member.MemberId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMembershipNumber;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.MemberRepository.IsUniqueEmail(member.Email, member.MemberId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueEmail;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.MemberRepository.IsUniqueMobile(member.Mobile, member.MemberId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueMobile;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (!(uow.MemberRepository.IsUniqueCnic(member.CNIC, member.MemberId)))
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.uniqueCnic;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                member.UpdatedOn = DateTime.Now;
                uow.MemberRepository.UpdateMember(member);
                uow.MemberRepository.Save();

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
        public HttpResponseMessage DeleteMember(Member member)
        {
            try
            {
                if (member.UpdatedBy == null || member.UpdatedBy <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.invalidUserId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                if (member.MemberId <= 0)
                {
                    response.Code = ApplicationConstants.errorCode;
                    response.Message = ApplicationConstants.EmptyId;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                member.UpdatedOn = DateTime.Now;
                uow.MemberRepository.DeleteMember(member.MemberId, (int)member.UpdatedBy);
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