using WorldFamily.User.Service.Dtos;

namespace WorldFamily.User.Service.Services
{
    public interface IUserService
    {
        AccountDetailDto GetAccountDetailsByPhoneAndRegion(string phone, string region);
    }
}