using dsknowledgetestsback.Constants;
using dsknowledgetestsback.Services;
using dsknowledgetestsback.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dsknowledgetestsback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = nameof(RolesConst.Admin))]
        [Route("GetActivatedUsers")]
        [HttpGet]
        public async Task<List<ShowUserViewModel>> GetActivatedUsers()
        {
            return await _userService.GetActivatedUsers();
        }

        [Authorize(Roles = nameof(RolesConst.Admin))]
        [Route("GetUnactivatedUsers")]
        [HttpGet]
        public async Task<List<ShowUserViewModel>> GetUnactivatedUsers()
        {
            return await _userService.GetUnactivatedUsers();
        }
    }
}