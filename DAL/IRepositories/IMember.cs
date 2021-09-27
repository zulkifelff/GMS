using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.ViewModels;
namespace DAL.IRepositories
{
    interface IMember
    {
        IEnumerable<LookupModel> GetMembers();
        Member GetMemberById(int? memberId);
        void InsertMember(Member member);
        void DeleteMember(int? memberId, int userId);
        void UpdateMember(Member member);
        void Save();
    }
}
