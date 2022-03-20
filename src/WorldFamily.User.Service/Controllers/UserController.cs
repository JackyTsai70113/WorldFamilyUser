using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldFamily.User.Service.Data;
using WorldFamily.User.Service.Dtos;
using WorldFamily.User.Service.Services;

namespace WorldFamily.User.Service.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("account-details")]
        public ActionResult<AccountDetailDto> GetAccountDetailByPhoneAndRegion(string phone, string region)
        {
            var accountDetail = userService.GetAccountDetailByPhoneAndRegion(phone, region);

            if (accountDetail == null)
            {
                return NotFound();
            }

            return accountDetail;
        }
    }
}

