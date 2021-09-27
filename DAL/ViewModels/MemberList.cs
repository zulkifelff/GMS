using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class MemberList
    {
        public int MemberId { get; set; }
        public string MembershipNumber { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public string CNIC { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
    }
}
