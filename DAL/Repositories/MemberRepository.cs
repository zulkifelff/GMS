using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.IRepositories;
using System.Data.Entity;
using DAL.ViewModels;
using Common;

namespace DAL.Repositories
{
    public class MemberRepository : IMember
    {
        private GMSEntities _context;
        public MemberRepository(GMSEntities context)
        {
            this._context = context;
        }
        public IEnumerable<LookupModel> GetMembers()
        {
            return _context.Members.Where(x => x.IsActive == true)
                .Select(x => new LookupModel
                {
                    Id = x.MemberId,
                    Name = x.Name
                }).ToList();
        }
        public IEnumerable<MemberList> GetMembersDetailData()
        {
            return _context.Members.Where(x => x.IsActive == true)
                .Select(x => new MemberList
                {
                    MembershipNumber = x.MembershipNumber,
                    MemberId = x.MemberId,
                    Name = x.Name,
                    Address = x.Address,
                    Email = x.Email,
                    CNIC = x.CNIC,
                    DOB = x.DOB,
                    Gender = x.Gender == true ? "Male" : "Female",
                    Mobile = x.Mobile,
                    Nationality = _context.Countries.Where(y => y.CountryId == x.Nationality).Select(z => z.Title).FirstOrDefault(),
                    ProfilePicture = x.ProfilePicture == null ? ApplicationConstants.DefaultUserImagePath : x.ProfilePicture,
                    
                }).ToList();
        }
        public Member GetMemberById(int? id)
        {
            return _context.Members.Find(id);
        }
        public void InsertMember(Member member)
        {
            _context.Members.Add(member);
        }
        public void DeleteMember(int? memberId, int userId)
        {
            //Member member = _context.Members.Find(memberId);
            //_context.Members.Remove(member);
            Member member = _context.Members.Find(memberId);
            if (member != null)
            {
                member.IsActive = false;
                member.UpdatedBy = userId;
                member.UpdatedOn = DateTime.Now;
                Save();
            }
        }

        public void UpdateMember(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
        }
        public void ModifyMember(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
        }
        //public void UpdateCredentials(Member member)
        //{
        //    Member currMember = GetMemberById(member.MemberId);
        //    if (currMember != null)
        //    {
        //        currMember.UpdatedBy = member.UpdatedBy;
        //        currMember.UpdatedOn = member.UpdatedOn;
        //        currMember.MembershipNumber = member.MembershipNumber;
        //        currMember.Password = member.Password;
        //        Save();
        //    }
        //    //_context.Entry(member).State = EntityState.Modified;
        //}

        public bool IsUniqueMembershipNumber(string memberNumber, int memberId)
        {
            Member member = _context.Members.Where(x => x.MembershipNumber == memberNumber && x.MemberId != memberId).FirstOrDefault();
            if (member != null)
                return false;

            return true;
        }

        public bool IsUniqueEmail(string email, int memberId)
        {
            Member member = _context.Members.Where(x => x.Email == email && x.MemberId != memberId).FirstOrDefault();
            if (member != null)
                return false;

            return true;
        }
        public bool IsUniqueMobile(string mobile, int memberId)
        {
            Member member = _context.Members.Where(x => x.Mobile == mobile && x.MemberId != memberId).FirstOrDefault();
            if (member != null)
                return false;

            return true;
        }
        public bool IsUniqueCnic(string cnic, int memberId)
        {
            Member member = _context.Members.Where(x => x.CNIC == cnic && x.MemberId != memberId).FirstOrDefault();
            if (member != null)
                return false;

            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
