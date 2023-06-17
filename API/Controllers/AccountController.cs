using API.Helpers;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserViewModel>> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                return Unauthorized("Incorrect email or password");
            }

            var result = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if (!result)
            {
                return Unauthorized("Incorrect email or password");
            }

            return Ok(
                new UserViewModel()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = TokenHelper.CreateToken(user, _configuration)
                }
            );
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> Register(RegisterViewModel registerViewModel)
        {
            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

            if (user != null)
            {
                return BadRequest("Email is in use");
            }

            user = new IdentityUser()
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest("The password is too weak");
            }

            return Ok(
                new UserViewModel()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = TokenHelper.CreateToken(user, _configuration)
                }
            );
        }

        [HttpGet]
        [Authorize]
        public ActionResult SecretInfo()
        {
            return Ok("Paul is the best beatle");
        }
    }
}
