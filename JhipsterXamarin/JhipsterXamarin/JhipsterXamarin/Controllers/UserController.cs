using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JhipsterXamarin.Models;
using JhipsterXamarin.Services;
using JhipsterXamarin.Exceptions;
using JhipsterXamarin.Utilities;
using JhipsterXamarin.Constants;

namespace JhipsterXamarin.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _log;
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> log, UserManager<UserModel> userManager, IUserService userService,
            IMapper mapper)
        {
            _log = log;
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("users")]
        public async Task<ActionResult<UserModel>> AddUser([FromBody] UserModel user)
        {
            _log.LogDebug($"REST request to save User : {user}");
            if (user.Id != null)
                throw new BadRequestAlertException("A new user cannot already have an ID", "userManagement",
                    "idexists");
            // Lowercase the user login before comparing with database
            if (await _userManager.FindByNameAsync(user.Login.ToLowerInvariant()) != null)
                throw new LoginAlreadyUsedException();

            await _userService.Add(user);
            return CreatedAtAction(nameof(GetUser), new { login = user.Login }, user)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert("userManagement.created", user.Login));
        }

        [HttpPut("users")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel user)
        {
            _log.LogDebug($"REST request to update User : {user}");
            var existingUser = await _userManager.FindByEmailAsync(user.Login);

            if (existingUser != null && !existingUser.Id.Equals(user.Id)) throw new LoginAlreadyUsedException();

            await _userService.Update(user);
            return ActionResultUtil.WrapOrNotFound(user)
                .WithHeaders(HeaderUtil.CreateAlert("userManagement.updated", user.Login));
        }

        [HttpGet("users")]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            _log.LogDebug("REST request to get a page of Users");
            return await _userService.GetAll();
        }

        [HttpGet("users/authorities")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = RolesConstants.ADMIN)]
        public ActionResult<IEnumerable<string>> GetAllAuthorities()
        {
            return Ok(_userService.GetAllAuthorities());
        }

        [HttpGet("users/{login}")]
        public async Task<IActionResult> GetUser([FromRoute] string login)
        {
            _log.LogDebug($"REST request to get User : {login}");
            var user = _userManager.Users.Where(user => user.Login == login);
            var result = await _userService.Get(login);
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("users/{login}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = RolesConstants.ADMIN)]
        public async Task<IActionResult> DeleteUser([FromRoute] string login)
        {
            _log.LogDebug($"REST request to delete User : {login}");
            await _userService.Delete(login);
            return Ok((HeaderUtil.CreateEntityDeletionAlert("userManagement.deleted", login)));
        }
    }
}