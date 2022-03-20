using WorldFamily.User.Service.Dtos;

namespace WorldFamily.User.Service.Services
{
    public interface IUserService
    {
        AccountDetailDto GetAccountDetailByPhoneAndRegion(string phone, string region);
    }
}