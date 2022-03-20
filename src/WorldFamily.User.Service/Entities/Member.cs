using System;
using System.ComponentModel.DataAnnotations;

namespace WorldFamily.User.Service.Entities
{
    public class Member : IEntity
    {
        public Member() { }

        public Member(string memberNO, DateTime joinDate, DateTime expiredDate, string memberStatus, string phoneNo, string dwePackage)
        {
            MemberNO = memberNO;
            JoinDate = joinDate;
            ExpiredDate = expiredDate;
            MemberStatus = memberStatus;
            PhoneNo = phoneNo;
            DWEPackage = dwePackage;
        }

        [Key]
        public string MemberNO { get; set; }

        public DateTime JoinDate { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string MemberStatus { get; set; }

        public string PhoneNo { get; set; }

        public string DWEPackage { get; set; }
    }
}