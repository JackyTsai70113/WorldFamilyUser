using System;
using System.ComponentModel.DataAnnotations;

namespace WorldFamily.User.Service.Entities
{
    public class Customer : IEntity
    {
        public Customer() { }

        public Customer(string memberNO, string contractNo, DateTime signDate, string contractStatus, string phoneNo, string dwePackage)
        {
            MemberNO = memberNO;
            ContractNo = contractNo;
            SignDate = signDate;
            ContractStatus = contractStatus;
            PhoneNo = phoneNo;
            DWEPackage = dwePackage;
        }

        public string MemberNO { get; set; }

        [Key]
        public string ContractNo { get; set; }

        public DateTime SignDate { get; set; }

        public string ContractStatus { get; set; }

        public string PhoneNo { get; set; }

        public string DWEPackage { get; set; }
    }
}