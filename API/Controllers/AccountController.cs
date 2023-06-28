using API.Helpers;
using DAL.Interface;
using Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            IEmailService emailService
        )
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
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

        [HttpPost("forgotpassword")]
        public async Task<ActionResult> ForgotPassword(
            ForgotPasswordViewModel forgotPasswordViewModel
        )
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);

            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var validator = new PasswordValidator<IdentityUser>();
            var validation = await validator.ValidateAsync(
                _userManager,
                null,
                forgotPasswordViewModel.Password
            );

            if (validation.Succeeded)
            {
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    new
                    {
                        password = forgotPasswordViewModel.Password,
                        email = forgotPasswordViewModel.Email,
                        token = token
                    },
                    protocol: HttpContext.Request.Scheme
                );

                _emailService.SendEmail(
                    forgotPasswordViewModel.Email,
                    "Password reset",
                    $"Someone is trying to change your password. If it is you, click this link to confirm password reset: <br /> <a href=\"{callbackUrl}\"> Reset Password </a>"
                );

                return Ok();
            }

            return BadRequest("The password is too weak");
        }

        [HttpGet("ResetPassword")]
        public async Task<ActionResult> ResetPassword(
            string password,
            string email,
            string token = null
        )
        {
            if (token == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.ResetPasswordAsync(user, token, password);

            return Redirect("https://localhost:4300/");
        }
    }
}
