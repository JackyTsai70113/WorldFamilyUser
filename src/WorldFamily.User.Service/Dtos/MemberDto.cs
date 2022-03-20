using System;
using System.Collections.Generic;

namespace WorldFamily.User.Service.Dtos
{
    public record AccountDetailDto(string PhoneNumber, string Region, bool IsMember, string MemberId, DateTime MembershipExpirationTime, bool IsCustomer, List<string> ContractId);
}