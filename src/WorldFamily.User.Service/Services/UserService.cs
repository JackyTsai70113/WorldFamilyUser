using System;
using System.Collections.Generic;
using System.Linq;
using WorldFamily.User.Service.Data;
using WorldFamily.User.Service.Dtos;

namespace WorldFamily.User.Service.Services
{
    public class UserService : IUserService
    {
        private readonly WorldFamilyDbContext context;

        public UserService(WorldFamilyDbContext context)
        {
            this.context = context;
        }

        public AccountDetailDto GetAccountDetailByPhoneAndRegion(string phone, string region)
        {
            var member = context.Members.Where(m => m.PhoneNo == phone).SingleOrDefault();

            if (member == null)
            {
                return null;
            }

            var customers = context.Customers.Where(c => c.PhoneNo == phone && c.ContractStatus == "Normal");

            bool isMember = member.MemberStatus == "Normal";
            bool isCustomer = customers.Any();
            DateTime expiredDate = member.ExpiredDate.ToUniversalTime();
            List<string> contractIds = customers.Select(c => c.ContractNo).ToList();

            var accountDetail = new AccountDetailDto(phone, region, isMember, member.MemberNO, expiredDate, isCustomer, contractIds);

            return accountDetail;
        }
    }
}